   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   &     OProfTest.MVVM.View.TestsCatalog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <	viewModel&clr-namespace:OProfTest.MVVM.ViewModel     5   	   3local!clr-namespace:OProfTest.MVVM.View     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   $��Microsoft YaHei)�5   	    � Title$ 	OProfTest��5   	   $	��700��5   	   $
��1100��5   	    � Icon$  ../../Image/mainIcon64.ico��5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��80 �6   6*   �� 5      6    � ColumnDefinitions 5   
   �� 5      $
��20px �6   6.   �� 5      6!   .+�5   
   �� $��2��6   $��0��6(   $��28(�65   $��SemiBold"�6C   $��Center=�6Y   $��Center=�6t      TitleTestsCatalog#���   6�   6�   �� 5   
   $��2q�6   $��1��6"   $��1��6/   .��5      �� ��. $��Disabled=�6�   ��6   �� *Tests+ �� SelectedItem 65   �� *SelectedTest+��5      �� 5      .R�5      �� $��Center=�6!    �� CornerRadius$ 10��5      $��0q�5       $
��12 0q�5!      $	��300��5"       � Cursor$
 Hand��5#      $	��250��5$      ._�5%      �� 	 �� Orientation$	 Vertical=�6)   .+�5&   "   ]� $	��200��6,   .+�5'   &   �� $	��200��6-   $ 10��6:   $��0q�6L   ��5(   *   �� 5)   .   
 j� Stretch$

 Fill=�69    �� ImageSource 6H   �� *
ImageBytes+ �� UpdateSourceTrigger$ PropertyChanged=�5+   '   5,   #   �� 5-   "   $��Center=�6-   $	��0 9q�6H   .+�5.   &   ��  �� TextAlignment$ Center=�60   $	��300��6G   $
��16px(�6S   $��SemiBold"�6c   
���   ���6�   �� *Title+$ PropertyChanged=�5/   #   50      51      52      ��54      �� 55      .R�56      � 57      59      5:      5;      5<      