   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApp1.DialogsWpfApp1.Dialogs  "    WpfApp1.Dialogs.OrdersDialog   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:WpfApp1.Dialogs       � Title$  OrdersDialog��$	��530��$	��500�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterOwner=�$��10q����#eee � 
ResizeMode$ NoResize=� � 	Resources    CloseButton �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color	   	Direction
   ShadowDepth   
BlurRadius   Opacity �� 	Resources  ProceedOrderButton  DeleteItemButton  ItemNameElement  ItemsInfoElement�� %�  &	        &	 J    &	 �    &	 �    &	 F    �� " ��.���  � ��$	 100����  � ��$ 30����  � ��$ SemiBold"���  � ��$ 16(���  � �� �#f99626��  � �� �#fff��  � ��$ Center=���  �@J�Effect   $ Black��$		 250Q�$
 3Q�$ 7Q�$	 0.1Q� ���� '��       " ��.���  �@��CornerRadius$ 5���� " ��.���  � ��$ 
0, 3, 0, 0q���  � ��$ 45����  � �� �#50c37f��  � �� �#fff��  � ��$ 23(���  � ��$ SemiBold"��� " ��.���  � ��$
 10,0q���  � ��$ 25����  � ��$ 25����  � �� �#E21B1A��  � �� �#fff��  � ��$
 Bold"� ���� '��       " ��.���  �@��CornerRadius$ 35���� " ��.���  � ��$	 310����  � ��$
 Wrap=���  �@��Padding$ 10, 0q���  � ��$ 16(��� " ��.���  � ��$ 14(���  � ��$ SemiBold"���  � ��$ 0, 0, 10, 0q�.���� .+��� $��0,0,0,1q����#aaa._�]� $��30��$��10, 15q�.+��� $
��Left=�$��Ваш заказ��$��23(�$��SemiBold"��� -   $��Right=�#��[  $��Закрыть X��$��Right=��� $
��Auto=�$
��Auto=� �� PanningMode$
 Both=�$	��340��.���� .+��� -   $��icOrdersElements������ .R��� $	��430������ .R��� .+��� $��
0, 0, 0, 1q����#eee$	��0,2q�._�]� .+��� $
��Left=�$��75��$��75��$	��Top=����� *Photo+�� $��Right=�.+�]� .+��� $
��Left=�#��[ ���� *Name+�� -   $��Right=�#��[ $��X��
 � Tag �� *Id+� $��10, 0q�.+��� #��[ ���� *Price+ �� StringFormat$ Цена: {0} тг.���� #��[ ���� *
Quantity+$ Количество: {0}���� $��Bottom=�.+��� $��
0, 1, 0, 0q����#eee$	��0,2q�._�]� $��31��$��10, 12q�.+��� $
��Left=�$��Сумма:��$��23(�$��SemiBold"�$	��200���� -    ��Name$ OrderSumElement��$��Right=�$��23(�$��SemiBold"�$��Right=����� *
OrderSum+$ 
 {0} тг.���� -   #��[ $0��*Подтвердить ваш заказ >�� K� Capitals$ AllSmallCaps=�