   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   %    OProfTest.MVVM.View.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <	viewModel&clr-namespace:OProfTest.MVVM.ViewModel     5   	   3local!clr-namespace:OProfTest.MVVM.View     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Title$ 	OProfTest��5   	   $	��650��6   $
��1000��6(    � Icon$  ../../Image/mainIcon64.ico��65   .��5      �  � RowDefinitions 5   
   �� 5      $��150px �6   6-   �� 5      $��100px �6   6-   �� 5      $��100px �6   6-   �� 5      $��100px �6   6-   �� 5      $��100px �6   6-   �� 5      6   .+�5   
   �� -   5        $��0��5      $��Добавить тест��6      CancelButtonTemplate#��[  66   $��Bottom=�6d   $��400px��6   $
��70px��6�    � Cursor$
 Hand��6�   6�   �� 5   
   -   5        $��1��5      $��Изменить тест��6   #��[  66   $��Bottom=�6d   $��400px��6   $
��70px��6�   $
 Hand��6�   6�   �� 5   
   -   5        $��2��5      $��Удалить тест��6   #��[  65   $��Bottom=�6c   $��400px��6~   $
��70px��6�   $
 Hand��6�   6�   �� 5   
   -   5        $��3��5      $'��!Список участников��6   #��[  6:   $��Bottom=�6h   $��400px��6�   $
��70px��6�   $
 Hand��6�   6�   �� 5   
   -   5        $��4��5      $��Результаты��6   #��[  63   $��Bottom=�6a   $��400px��6|   $
��70px��6�   $
 Hand��6�   6�   5      5      