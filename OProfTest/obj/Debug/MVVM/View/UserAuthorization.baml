   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   E'clr-namespace:OProfTest.MVVM.ComponentsOProfTest.MVVM.Components  5   	   +    %OProfTest.MVVM.View.UserAuthorization   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -local!clr-namespace:OProfTest.MVVM.View  5   	   8
components'clr-namespace:OProfTest.MVVM.Components  5   	     � Title$  Authorization��5
   	   $	��420��6   $	��350��6,    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � Icon$  ../../Image/mainIcon64.ico��5   	   $��Microsoft YaHei)�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��25 �6   6*   �� 5      $��30 �6   6*   �� 5      $	��100 �6   6+   �� 5      $��60 �6   6*   �� 5      $��60 �6   6*   �� 5      $��80 �6   6*   �� 5      $��40 �6   6*   .+�5   
   �� -   5        $��0��5      $	��Top=�6   $��Right=�66   $��0q�6R   $��25��6f   $��25��6q    � Cursor$
 Hand��6}   ��5      �� 5       �� ImageSource$ ../../Image/closeIcon.jpg��6   5      �� 5   
   $��1��6   $
��22px(�6!   $��SemiBold"�61   $��Center=�6G   $��Center=�6b      Autorization#���   6   6�   �� 5    
   $ ��../../Image/mainIcon64.jpg��6   $��2��64   $��45��6A   $��45��6L   6Z   �� 5!   
   $��3��6O   $��Center=�6\   $
��40px��6w   $��250px��6�   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Логин��6�    % !MaterialDesignFloatingHintTextBox#��[ 6�   ��6   �� *Login+	 �� UpdateSourceTrigger$	 PropertyChanged=�6�   3   -OProfTest.MVVM.Components.BindablePasswordBox  5"   
   $��4��6J   $��Center=�6W   $
��40px��6r   $��250px��6�   $ Пароль��6�   
   Password
 6*   �� *	Password1+6�   �� 5#   
   $��5��63   $��Bottom=�6@   $��250px��6[   $
��45px��6i   $��20(�6w   $��SemiBold"�6�   $
 Hand��6�     EnterButtonTemplate#��[ 6�    	 Enter#���  6�   ��6   �� *Authorization+6�   �� 5$   
   -   5        $��6��5$      $��250px��6   $
��30px��6,   $��12(�6:   $��Medium"�6H   $��Center=�6\     BackToRegister#���  6�   ��6w   Z� ��6�   Z� 6�   5%      5&      