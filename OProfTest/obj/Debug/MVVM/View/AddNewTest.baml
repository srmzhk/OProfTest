   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   $    OProfTest.MVVM.View.AddNewTest   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   6	viewModel&clr-namespace:OProfTest.MVVM.ViewModel  5   	   -local!clr-namespace:OProfTest.MVVM.View  5   	     � WindowStartupLocation$  CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	    � Title$ 	OProfTest��5   	   $	��700��6   $	��700��6(    � Icon$  ../../Image/mainIcon64.ico��64   .��5      �  � RowDefinitions 5   
   �� 5      $��230px �6   6-   �� 5      $
��70px �6   6,   �� 5      $��220px �6   6-   �� 5      $��100px �6   6-   .+�5   
   �� $��340px��6   $��210px��6   $��0q�6.   
�������
������ ��6o   �� *OpenFileDialog+.��5      �� $	��200��6   $	��300��5       �� CornerRadius$ 10��5      $	��1.6q�5   	   
���������5      �� 5       j� Stretch$
 Fill=�6!    �� ImageSource 60   �� *	ImagePath+ �� UpdateSourceTrigger$ PropertyChanged=�5      5      �� 5   
   $
��16px(�6   $��1��6"   $��10px 0q�6/   $��Center=�6|   $
��40px��6�   $��500px��6�   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Название��6�    %  !MaterialDesignFloatingHintTextBox#��[  6�   ��6@   �� *Title+$ PropertyChanged=�6  �� 5   
   $
��16px(�6   
 �� TextWrapping$

 Wrap=�6"   
���   �$��1q�6J   $
��10pxq�6^   $��2��6m   $��Center=�6�   $��200px��6�   $��500px��6�   $	 Описание��6�   #��[  6  ��6z   �� *Description+$ PropertyChanged=�6[  �� 5    
   $��3��6-   $��Добавить тест��6:     AddButtonTemplate#��[ 6R   $��Center=�6}   $��400px��6�   $
��70px��6�    � Cursor$
 Hand��6�   ��6   �� *	AddTest+6�   5!      5"      