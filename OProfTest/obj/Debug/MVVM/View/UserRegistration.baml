   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   C&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   E'clr-namespace:OProfTest.MVVM.ComponentsOProfTest.MVVM.Components  5	   	   *    $OProfTest.MVVM.View.UserRegistration   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -local!clr-namespace:OProfTest.MVVM.View  5   	   6	viewModel&clr-namespace:OProfTest.MVVM.ViewModel  5   	   8
components'clr-namespace:OProfTest.MVVM.Components  5	   	     � Title$  Authorization��5   	   $	��590��6   $	��350��6,    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � Icon$  ../../Image/mainIcon64.ico��5   	   $��Microsoft YaHei)�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��25 �6   6*   �� 5      $��30 �6   6*   �� 5      $	��100 �6   6+   �� 5      $��60 �6   6*   �� 5      $��60 �6   6*   �� 5      $��60 �6   6*   �� 5      $��60 �6   6*   �� 5      $��60 �6   6*   �� 5      $��80 �6   6*   �� 5      $��40 �6   6*    � ColumnDefinitions 5   
   �� 5      6!   �� 5       6!   .+�5"   
   �� -   5        $��0��5"      $��2��6   $	��Top=�62   $��Right=�6J   $��0q�6f   $��25��6z   $��25��6�    � Cursor$
 Hand��6�   ��5#      �� 5$       �� ImageSource$ ../../Image/closeIcon.jpg��6   5&      �� 5'   
   $��1��6   $��2��6!   $
��22px(�65   $��SemiBold"�6E   $��Center=�6[   $��Center=�6v      Registration#���   6�   6�   �� 5(   
   $ ��../../Image/mainIcon64.jpg��6   $��2��64   $��45��6A   $��45��6L   $��2��6X   6n   �� 5)   
   $��3��6R   $��0��6_   $��Center=�6o   $
��40px��6�   $	��115��6�   $��42 0 0 0q�6�   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Имя��6�    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *	FirstName+
 �� UpdateSourceTrigger$
 PropertyChanged=�6  �� 5*   
   $��3��6Q   $��1��6^   $��Center=�6n   $
��40px��6�   $	��115��6�   $��0 0 42 0q�6�   $	 Фамилия��6�   #��[ 6�   ��6   �� *
LastName+$
 PropertyChanged=�6  �� 5+   
   $��4��6L   $��2��6Z   $��Center=�6n   $
��40px��6�   $��250px��6�   $	 Возраст��6�   #��[ 6�   ��6   �� *Age+$
 PropertyChanged=�6  �� 5,   
   $��5��6N   $��2��6\   $��Center=�6p   $
��40px��6�   $��250px��6�   $	 
Логин��6�   #��[ 6�   ��6   �� *Login+$
 PropertyChanged=�6  3   -OProfTest.MVVM.Components.BindablePasswordBox  5-   
   $��6��6J   $��2��6X   $��Center=�6l   $
��40px��6�   $��250px��6�   $	 Пароль��6�      Password 6*   �� *	Password1+6�     5.   
   $��7��6J   $��2��6W   $��Center=�6k   $
��40px��6�   $��250px��6�   $%	 Повторите пароль��6�    6*   �� *	Password2+6�   �� 5/   
   $��8��6.   $��2��6;   $��Bottom=�6O   $��250px��6j   $
��45px��6x   $��18(�6�   $��SemiBold"�6�   $
 Hand��6�     EnterButtonTemplate#��[ 6�     CreateAccount#���  6�   ��6   �� *
Register+6  �� 50   
   -   5        $��9��50      $��2��6   $��250px��62   $
��30px��6@   $��12(�6N   $��Medium"�6\   $��Center=�6p     BackToAuthorization#���  6�   ��6�   Z� ��6�   Z� 6  51      52      