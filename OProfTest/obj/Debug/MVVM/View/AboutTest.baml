   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   #    OProfTest.MVVM.View.AboutTest   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <	viewModel&clr-namespace:OProfTest.MVVM.ViewModel     5   	   3local!clr-namespace:OProfTest.MVVM.View     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   $��Times New Roman)�5   	    � Title$ 	OProfTest��5   	   $	��700��5   	   $
��1000��5   	    � Icon$  ../../Image/mainIcon64.ico��5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��80 �6   6*   �� 5      $	��220 �6   6+   �� 5      $	��250 �6   6+   �� 5      $	��100 �6   6+   �� 5      6   .+�5   
   �� -   5        $��0��5      $
��50px��6   $
��50px��6*   $
��Left=�68   $��15 0 0 0q�6S   $��../../Image/backArrow.png��6e    � Cursor$
 Hand��6�   6�   �� 5   
   $��700px��6   $��0��6"   $��28(�6/   $��SemiBold"�6=   $��Center=�6S   $��Center=�6n    �� TextAlignment$ Center=�6�   ��6�   �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��1��6   $	��200��6   $	��300��6+    �� CornerRadius$ 10��67   $��0q�6I   ��5      �� 5      	 j� Stretch$
	 Fill=�6   
 �� ImageSource
 6,   �� *
ImageBytes+$ PropertyChanged=�5      �� 5   
   $��600px��6   $��200px��6"   $��2��61   $��16(�6>   $��SemiBold"�6L   $
��Wrap=�6b   $��Center=�6v   $��Stretch=�6�   ��6�   �� *Description+$ PropertyChanged=�6�   �� 5    
   $��3��6/   $��Начать тест��6<      StartButtonTemplate#��[  6R   $��26(�6   $��Center=�6�   $��400px��6�   $
��70px��6�   $
 Hand��6�   ��6   �� *	StartTest+6�   5!      5"      