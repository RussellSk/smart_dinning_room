   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5clr-namespace:WpfApp1.ConverterWpfApp1.Converter  7 clr-namespace:WpfApp1.PropertiesWpfApp1.Properties      WpfApp1.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1     /conclr-namespace:WpfApp1.Converter     7
Properties clr-namespace:WpfApp1.Properties     -     �Name$  window�� � Title$ 
MainWindow��$��469.922��$��720.313�� � WindowState$ 	Maximized=� � WindowStyle$
 None=� � 	Resources &    WpfApp1.Converter.WidthConverter   WidthConverter  myButtonStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color	   	Direction
   ShadowDepth   
BlurRadius   Opacity �� Triggers P� Property P� Value  leftMenuButton  foodItemText  foodItemButton  foodItemIncrementialElement�� %  &	        &	      &	 U    &	 �    &	 �    &	 Z      �� " ��.���  � ��$ 30����  � ��$ 80����  � ��
 ��  ���  � �� �#fff��  � ��$ 10q���  � ��$ 12(���  �@J�Effect   $ Black��$		 320Q�$
 3Q�$ 5Q�$	 0.5Q� P�  �@J�IsMouseOver . . ���  � ��
 � � ���  � ��$ 15(���  � ��
 �<���� " ��.���  � �� �#fff��  � ��$ 16(���  � ��$
 Left=���  � ��$ 10, 10q���  � ��$
 0, 2q���  � �� �#eee��  � ��$ 1q���  � �� �#333��  � ��$ 50����  � ��$ Center=���  � ��$ DemiBold"� P�  �@J�IsMouseOver . . ���  � ��
 �   ��� " ��.���  � ��$ 16(��� " ��.���  � �� �#80b95a��  � �� �#fff��  � ��$ 16(���  � ��$ SemiBold"���  � ��$ 37���� " ��.���  � ��$ 20(���  � ��$ Black"���  � ��$ Center=���  � ��$ Center=�.��]� .+�]� $	��Top=�$��68����.  $	��Top=����#f6f9fc J� BitmapEffect F�  F� Color$ Black�� F� 	Direction$ 1Q� F� ShadowDepth$ 0Q� F� Opacity$	 0.1Q� F� Softness$ 15Q�.+�� $
��Left=�.+��� $��58��$	��Top=�$��67��$��Resources/logo.png��$��20,5,0,0q��� $��68��$
��Wrap=�$��SmartSchool��$	��155��$	��Top=�$��12,0,0,0q�$��24(� �� TextAlignment$ Center=�$��Segoe UI Historic)����#3d92e1 �� Padding$ 0,19,0,0q�$
��Bold"��� -   $��Center=�$��Right=�$��38��$	��170�����#f99626$��70, 0q� J� Effect   $ Black��$		 250Q�$
 3Q�$ 7Q�$	 0.1Q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 5��.��� $	��250��.+��� $��25��$��25��$	��7,5q�$!��Resources/shopping-cart.png���� $
��Wrap=�$��Корзина��$	��Top=�
�������$��24(��� ���#fff$��25��$��27��$��Center=� �� CornerRadius$ 4��$��7,0,0,0q�._��� -    ��Name$ orderElement��$��Center=�$��16(�$��Center=�$��SemiBold"����� *OrderAmount+�� $
��Left=����#f6f9fc$	��243������ *ActualHeight+ �� ElementName$ window�� �� Mode$ OneWay=�.+���  �� Padding$ 0, 45, 0, 0q�._��� -   $��
icLeftMenu������ .R��� -   #��[ 
 � Tag �� *Id+.��� .+��� ���� *Photo+�� $��Center=�$ 15, 0q����� *Name+�� $��Disabled=�$
��Auto=� �� PanningMode$
 Both=�.��� $��35, 35q�.+��� -   $��icMainItemsMenu������ .R�� ���� *Width+$ window��$ OneWay=�  �� 	Converter#  [  ���� .R��� $	��250�����#efefef$��2q�$ 4��$ 20, 20q�$��20, 20q�$	��Top=�._��� .+��� $	��200��$	��200������ *Photo+�� #��[ $��Center=�$
��Wrap=����� *Name+�� #��[ $��Center=����� *Price+! �� StringFormat$! Цена: {0} тг.��]� $��0, 10, 0, 0q�.+��� -   #��[ $��Right=�$��Выбрать�� �� *Id+ ��%9   '��       �� " ��.���  �@��CornerRadius$ 5��