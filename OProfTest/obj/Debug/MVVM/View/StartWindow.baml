   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   %    OProfTest.MVVM.View.StartWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <	viewModel&clr-namespace:OProfTest.MVVM.ViewModel     5   	   3local!clr-namespace:OProfTest.MVVM.View     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Title$ 	OProfTest��5   	   $	��650��6   $
��1000��6(    � Icon$  ../../Image/mainIcon64.ico��65   .��5      �  � RowDefinitions 5   
   �� 5      $
��70px �6   6,   �� 5      $��310px �6   6-   �� 5      $��100px �6   6-   �� 5      $��100px �6   6-   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   .+�5   
   ��  	  Hello#���   6   $��0��63   $��Center=�6@   $��Center=�6]   $
��26px(�6x   $
��Bold"�6�   $��Microsoft YaHei)�6�   6�   �� 5   
     
AboutStart#���  6   $��1��68   $��480px��6E   $
��Wrap=�6S   $��Center=�6g   $��Center=�6�    �� TextAlignment$ Center=�6�   $
��20px(�6�   $��Times New Roman)�6�   6�   �� 5   
   -   5          Autorization#���  5        StartButtonTemplate#��[ 6:   $��2��6g   $��400px��6t   $
��70px��6�    � Cursor$
 Hand��6�   6�   �� 5   
   -   5         
 Cancel#���  5        CancelButtonTemplate#��[ 64   $��3��6b   $��400px��6o   $
��70px��6}   $
 Hand��6�   6�   5      5      