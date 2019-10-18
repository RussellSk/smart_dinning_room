using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetSDKCS;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;

namespace TerminalListener
{
    public class BaseProccess
    {
        public IntPtr loginID = IntPtr.Zero;
        public IntPtr realLoadID = IntPtr.Zero;
        public fDisConnectCallBack disConnectCallBack;
        public fHaveReConnectCallBack haveReConnectCallBack;
        public fAnalyzerDataCallBack analyzerDataCallBack;
        private fMessCallBackEx m_AlarmCallBack;
        public NET_DEVICEINFO_Ex device;
        private fHaveReConnectCallBack m_ReConnectCallBack;

        public static ArrayList device_channel = new ArrayList();
        public ArrayList arrayList_userID = new ArrayList();
        public bool IsListen = false;

        public String nedded_user_id = null;
        //event queue.thread is safe.
        private ConcurrentQueue<AlarmMsg> m_AlarmMsgQueue = new ConcurrentQueue<AlarmMsg>();

        ~BaseProccess()
        {
            Console.WriteLine("==== BASEPROC Destructor");
            Disconnect();
        }

        public BaseProccess()
        {

            disConnectCallBack = new fDisConnectCallBack(DisConnectCallBack);
            haveReConnectCallBack = new fHaveReConnectCallBack(ReConnectCallBack);
            analyzerDataCallBack = new fAnalyzerDataCallBack(AnalyzerDataCallBack);
            m_AlarmCallBack = new fMessCallBackEx(AlarmCallBackEx);
            m_ReConnectCallBack = new fHaveReConnectCallBack(ReConnectCallBack);
            NETClient.SetDVRMessCallBack(m_AlarmCallBack, IntPtr.Zero);
            NETClient.SetAutoReconnect(m_ReConnectCallBack, IntPtr.Zero); //set reconnect callback.

            try
            {
                NETClient.Init(disConnectCallBack, IntPtr.Zero, null);
                NETClient.SetAutoReconnect(haveReConnectCallBack, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                Console.WriteLine("init = " + ex.Message);
                //Process.GetCurrentProcess().Kill();
            }
        }

        public void Disconnect()
        {
            try
            {
                NETClient.StopListen(loginID);
            }
            catch (Exception ex)
            {
            }
            
            if (realLoadID != IntPtr.Zero)
            {
                NETClient.StopLoadPic(realLoadID);
            }
            if (IsListen)
            {
                NETClient.StopListen(loginID);
            }
            if (loginID != IntPtr.Zero)
            {
                NETClient.Logout(loginID);
            }
        }

        private int AnalyzerDataCallBack(IntPtr lAnalyzerHandle, uint dwEventType, IntPtr pEventInfo, IntPtr pBuffer, uint dwBufSize, IntPtr dwUser, int nSequence, IntPtr reserved)
        {
            if (realLoadID != lAnalyzerHandle)
            {
                return 0;
            }
            switch (dwEventType)
            {
                case (uint)EM_EVENT_IVS_TYPE.ACCESS_CTL:
                    NET_DEV_EVENT_ACCESS_CTL_INFO info = (NET_DEV_EVENT_ACCESS_CTL_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_ACCESS_CTL_INFO));

                    Console.WriteLine("\ncase (uint)EM_EVENT_IVS_TYPE.ACCESS_CTL");
                    Console.WriteLine("NET_DEV_EVENT_ACCESS_CTL_INFO:");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("UTC: " + info.UTC.ToString());
                    Console.WriteLine("szUserID: " + info.szUserID);
                    Console.WriteLine("szCardName: " + info.szCardName);
                    Console.WriteLine("emOpenMethod: " + info.emOpenMethod.ToString());
                    Console.WriteLine("emAttendanceState: " + info.emAttendanceState.ToString());

                    break;
                default:
                    break;
            }

            return 0;
        }

        private void ReConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            string ip = Marshal.PtrToStringAnsi(pchDVRIP);
            try
            {
                NETClient.StartListen(lLoginID);
            }
            catch (Exception ex)
            {
                if (ex is NETClientExcetion)
                {
                    Console.WriteLine("ReConnect error = " + ip);
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }

            this.UpdateDisConnectedUI();
        }

        private void UpdateReConnectedUI()
        {
            Console.WriteLine("FaceAttendance -- OnLine");
        }

        private void DisConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            this.UpdateDisConnectedUI();
        }

        private void UpdateDisConnectedUI()
        {
            Console.WriteLine("FaceAttendance -- offline");
        }

        public void LoginDevice(String device_ip, String device_port, String device_user, String device_pwd)
        {
            if (IntPtr.Zero == loginID)
            {
                ushort port = 0;
                try
                {
                    port = Convert.ToUInt16(device_port);
                }
                catch
                {
                    Console.WriteLine("Input port error");
                    return;
                }
                device = new NET_DEVICEINFO_Ex();
                // 172.5.2.136
                loginID = NETClient.Login(device_ip, port, device_user, device_pwd, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref device);
                if (IntPtr.Zero == loginID)
                {
                    Console.WriteLine(NETClient.GetLastError());
                    return;
                }
                Console.WriteLine("\nLogin success");
            }
            else
            {
                NETClient.StopListen(loginID);
                bool result = NETClient.Logout(loginID);
                if (!result)
                {
                    Console.WriteLine(NETClient.GetLastError());
                    return;
                }
                loginID = IntPtr.Zero;
                Console.WriteLine("Logout");
            }
        }

        public void StartListen()
        {
            //NETClient.SetDVRMessCallBack(AlarmCallBackEx, IntPtr.Zero);
            
            if (NETClient.StartListen(this.loginID))
            {
                this.nedded_user_id = null;
                IsListen = true;
            }

        }

        public void StopListen()
        {
            try
            {
                NETClient.StopListen(loginID);
            } 
            catch (Exception ex)
            {
                Console.WriteLine("StopListen Error: " + ex.Message);
            }
        }

        public bool fMessCallBackEx(int lCommand, IntPtr lLoginID, IntPtr pBuf, uint dwBufLen, IntPtr pchDVRIP, int nDVRPort, bool bAlarmAckFlag, int nEventID, IntPtr dwUser)
        {
            //Console.WriteLine("\nNEW EVENT:");
            //Console.WriteLine("lCommand = " + string.Format("0x{x16}", lCommand));
            //Console.WriteLine("pchDVRIP = " + Marshal.PtrToStringAnsi(pchDVRIP));
            //Console.WriteLine("nEventID = " + nEventID);
            
            switch (lCommand)
            {
                case (int)NetSDKCS.EM_ALARM_TYPE.ALARM_CARD_RECORD:
                    NET_DEV_EVENT_ACCESS_CTL_INFO info = (NET_DEV_EVENT_ACCESS_CTL_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_DEV_EVENT_ACCESS_CTL_INFO));
                    Console.WriteLine(info.szUserID);
                    Console.WriteLine(info.szCardName);
                    Console.WriteLine(info.emOpenMethod.ToString());
                    Console.WriteLine(info.emAttendanceState.ToString());
                    break;
            }
            return true;
        }

        private bool AlarmCallBackEx(int lCommand, IntPtr lLoginID, IntPtr pBuf, uint dwBufLen, IntPtr pchDVRIP, int nDVRPort, bool bAlarmAckFlag, int nEventID, IntPtr dwUser)
        {
            Console.WriteLine("Enter");
            AlarmMsg alarmMsg = new AlarmMsg();
            alarmMsg.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            alarmMsg.LoginID = lLoginID;
            alarmMsg.AlarmType = (EM_ALARM_TYPE)lCommand;
            alarmMsg.IP = Marshal.PtrToStringAnsi(pchDVRIP);
            alarmMsg.AlarmInfo = GetObject(lCommand, pBuf, (int)dwBufLen);
            alarmMsg.Length = (int)dwBufLen;

            Console.WriteLine("\nNEW EVENT:");
            Console.WriteLine("lCommand = " + string.Format("0x{0:x16}", lCommand));
            Console.WriteLine("pchDVRIP = " + Marshal.PtrToStringAnsi(pchDVRIP));
            Console.WriteLine("nDVRPort = " + nDVRPort);
            Console.WriteLine("nEventID = " + nEventID);
            Console.WriteLine("Time = " + alarmMsg.Time);
            Console.WriteLine("dwUser = " + (ulong)dwUser);
            Console.WriteLine(alarmMsg.AlarmInfo == null);

            try
            {
                if ((EM_ALARM_TYPE)lCommand == EM_ALARM_TYPE.ALARM_ACCESS_CTL_EVENT && alarmMsg.AlarmInfo != null)
                {
                    NET_ALARM_ACCESS_CTL_EVENT_INFO info = (NET_ALARM_ACCESS_CTL_EVENT_INFO)alarmMsg.AlarmInfo;
                    if (info.szUserID[0] != 0)
                    {
                        this.nedded_user_id = Encoding.UTF8.GetString(info.szUserID);
                    }
                    //String postParams = "&emOpenMethod=" + info.emOpenMethod.ToString() + "&nDoor=" + info.nDoor.ToString() + "&szUserID=" + Encoding.UTF8.GetString(info.szUserID) +
                    //    "&szDoorName=" + Encoding.UTF8.GetString(info.szDoorName) + "&szCardNo=" + info.szCardNo + "&pchDVRIP=" + Marshal.PtrToStringAnsi(pchDVRIP) +
                    //    "&nDVRPort=" + nDVRPort.ToString() + "&nEventID=" + nEventID.ToString() + "&lCommand=" + lCommand.ToString();

                    //PostAsync(this.apiURL, postParams, "application/x-www-form-urlencoded");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReferenceException catched... working over");
            }
            catch (Exception e)
            {
                Console.WriteLine("PostAsync part ex: " + e.Message);
            }

            // m_AlarmMsgQueue.Enqueue(alarmMsg);
            Console.WriteLine("Leave");

            return true;
        }

        private object GetObject(int type, IntPtr buffer, int len)
        {
            Console.WriteLine("Enter");
            Object obj = null;
            switch ((EM_ALARM_TYPE)type)
            {
                case EM_ALARM_TYPE.ALARM_ACCESS_CTL_EVENT:
                    {
                        // Получение сообщения о попытке авторизации через: FACEID, FINGERPRINT, CARD

                        NET_ALARM_ACCESS_CTL_EVENT_INFO info = new NET_ALARM_ACCESS_CTL_EVENT_INFO();
                        info = (NET_ALARM_ACCESS_CTL_EVENT_INFO)Marshal.PtrToStructure(buffer, typeof(NET_ALARM_ACCESS_CTL_EVENT_INFO));

                        Console.WriteLine();
                        Console.WriteLine("emType = " + info.emOpenMethod);
                        Console.WriteLine("nDoor = " + Encoding.UTF8.GetString(info.szReaderID));
                        Console.WriteLine("szUserID = " + Encoding.UTF8.GetString(info.szUserID));
                        Console.WriteLine("szDoorName = " + Encoding.UTF8.GetString(info.szDoorName));
                        Console.WriteLine("szCardNo = " + info.szCardNo);
                        
                        obj = info;
                        break;
                    }
                default:
                    break;
            }

            Console.WriteLine("Leave");
            return obj;
        }

    }

    public class AlarmMsg
    {
        public string IP { get; set; }
        public IntPtr LoginID { get; set; }
        public EM_ALARM_TYPE AlarmType { get; set; }
        public object AlarmInfo { get; set; }
        public int Length { get; set; }
        public string Time { get; set; }
    }

}
