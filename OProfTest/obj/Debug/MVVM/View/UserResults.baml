   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   %    OProfTest.MVVM.View.UserResults   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   6	viewModel&clr-namespace:OProfTest.MVVM.ViewModel  5   	   -local!clr-namespace:OProfTest.MVVM.View  5   	     � WindowStartupLocation$  CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   $��Microsoft YaHei)�5   	    � Title$ 	OProfTest��5   	   $	��700��6   $
��1200��6(    � Icon$  ../../Image/mainIcon64.ico��65   .��5      �  � RowDefinitions 5   
   �� 5      $��100px �6   6-   �� 5      $��400px �6   6-   �� 5      $��150px �6   6-   �� 5      6    � ColumnDefinitions 5   
   �� 5      $
��50px �6   6.   �� 5      $	��340 �6   6-   �� 5      $	��300 �6   6-   �� 5      6!   �� 5      $
��50px �6   6.   .+�5   
   �� $
��Wrap=�6   $��1��6(   $��0��68   $
��40px��6E   $��420px��6S   $
��26px(�6a   $��SemiBold"�6q    �� TextAlignment$ Center=�6�   $��Center=�6�   $��Center=�6�   $��Участники��6�   6�   �� 5   
   $
��Wrap=�6   $��2��6(   $��0��68   $
��40px��6E   $��420px��6S   $
��26px(�6a   $��SemiBold"�6q   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��
Тесты��6�   6�   �� 5   
   $
��Wrap=�6   $��3��6(   $��0��68   $
��40px��6E   $��420px��6S   $
��26px(�6a   $��SemiBold"�6q   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��Результат��6�   6�   &   System.Windows.Controls.DataGrid  5   
   $��0 0 20 0q�6      AutoGenerateColumns .  $
��Left=�6h   
���   �$��1��6�   $��1��6�   
�������
���   � �� SelectedItem 6%   �� *SelectedUser+��6�   �� *Users+	   ColumnHeaderStyle	 5       �� 5!      
 �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"
  6   .�5"      ��  �� Property � �� �� Value
 �   �63   �� 5#       � ��
 �����63   �� 5$       � ��$ Center=�6<   5%         Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ Фамилия��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 66   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *	FirstName+  5)      $ Имя��6%   $	 150 62    6>   �� *
LastName+5+        5,   
   $��Right=�6    .  
���   �$��2��6�   $��1��6�   
�������
���   ���66   �� *Tests+	 5-      �� 5.      "
  6   .�5/      ��  � ��
 �   �63   �� 50       � ��
 �����63   �� 51       � ��$ Center=�6<   52       54        55      $ Тест��6%   $	 300 63    6?   �� *Title+57        58   
   $��Right=�6    .  
���   �$��3��6�   $��1��6�   
�������
���   ���66   �� *	Results+	 59      �� 5:      "
  6   .�5;      ��  � ��
 �   �63   �� 5<       � ��
 �����63   �� 5=       � ��$ Center=�6<   5>       5@        5A      $ Result��6%   $	 260 65    6A   �� *Description+  5B      $
 Date��6%   $	 170 63    6?   �� *
ResultDate+5D      �� 5E   
   -   5        $��1��5E      $��3��6!   $��2��65   $��
Назад��6B   $��28(�6R      CancelButtonTemplate#��[  6`   $��Center=�6�   $��Center=�6�   $��350px��6�   $
��70px��6�    � Cursor$
 Hand��6�   6�   5F      5G      