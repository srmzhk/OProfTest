   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   $    OProfTest.MVVM.View.DeleteTest   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   6	viewModel&clr-namespace:OProfTest.MVVM.ViewModel  5   	   -local!clr-namespace:OProfTest.MVVM.View  5   	     � WindowStartupLocation$  CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	    � Title$ 	OProfTest��5   	   $	��350��6   $	��700��6(    � Icon$  ../../Image/mainIcon64.ico��64   .��5      �  � RowDefinitions 5   
   �� 5      $��100px �6   6-   �� 5      $��100px �6   6-   �� 5      $��100px �6   6-   �� 5      $��100px �6   6-   .+�5   
   �� $
��Wrap=�6   $��0��6(   $
��50px��65   $��420px��6C   $
��22px(�6Q   $��SemiBold"�6a    �� TextAlignment$ Center=�6w   $��Bottom=�6�   $��Center=�6�   �� 6�   $>�8Выберите тест для его удаления��6�   6�   �� 5   
    �� DisplayMemberPath$ Title��69   $��1��6�   $��400px��6�   
���   �$��Center=�6�    �� SelectedItem 6   �� *SelectedTest+��6S   �� *Tests+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��2��60   $��Удалить тест��6=      DeleteButtonTemplate#��[  6T   $��Center=�6�   $��400px��6�   $
��70px��6�   	 � Cursor$
	 Hand��6�   ��6   �� *
DeleteTest+6�   5      5      