   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   '    !OProfTest.MVVM.View.ClientsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   6	viewModel&clr-namespace:OProfTest.MVVM.ViewModel  5   	   -local!clr-namespace:OProfTest.MVVM.View  5   	     � WindowStartupLocation$  CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $��Microsoft YaHei)�5   	    � Title$ 	OProfTest��5   	   $	��700��6   $
��1200��6(    � Icon$  ../../Image/mainIcon64.ico��65   .��5      �  � RowDefinitions 5   
   �� 5      $��100px �6   6-   �� 5      $��400px �6   6-   �� 5      $��150px �6   6-   �� 5      6    � ColumnDefinitions 5   
   �� 5      $��100px �6   6/   �� 5      6!   �� 5      6!   �� 5      $��100px �6   6/   .+�5   
   �� $
��Wrap=�6   $��1��6(   $��2��68   $��0��6L   $
��40px��6Y   $��420px��6g   $
��26px(�6u   $��SemiBold"�6�    �� TextAlignment$ Center=�6�   $��Center=�6�   $��Center=�6�   $'��!Список участников��6�   6  &   System.Windows.Controls.DataGrid  5   
   $��Center=�6:      AutoGenerateColumns . 
���   �$��2��6�   $��1��6�   $��1��6�   
�������
���   � �� SelectedItem 6   �� *SelectedUser+��6^   �� *Users+	   ColumnHeaderStyle	 5      �� 5      
 �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"
  6   .�5      ��  �� Property � �� �� Value
 �   �63   �� 5        � ��
 �����63   �� 5!       � ��$ Center=�6<   5"      5$      �� 5%   
   $��1��60   $��2��6@   $)��#Изменить участника��6M      StartButtonTemplate#��[  6j   $��Center=�6�   $��Center=�6�   $��350px��6�   $
��70px��6�    � Cursor$
 Hand��6�   ��6   �� *
ChangeUser+6�   �� 5&   
   $��2��60   $��2��6@   $'��!Удалить участника��6M     DeleteButtonTemplate#��[ 6i   $��Center=�6�   $��Center=�6�   $��350px��6�   $
��70px��6�   $
 Hand��6�   ��6   �� *
DeleteUser+6�   5'      5(      