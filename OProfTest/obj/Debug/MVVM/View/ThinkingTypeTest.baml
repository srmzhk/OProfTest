   M S B A M L   `   `   `  ����D  @OProfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:OProfTest.MVVM.ViewModelOProfTest.MVVM.ViewModel  5   	   9!clr-namespace:OProfTest.MVVM.ViewOProfTest.MVVM.View  5   	   *    $OProfTest.MVVM.View.ThinkingTypeTest   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <	viewModel&clr-namespace:OProfTest.MVVM.ViewModel     5   	   3local!clr-namespace:OProfTest.MVVM.View     5   	     � WindowStartupLocation$  CenterScreen=�5	   	   $��Times New Roman)�5
   	    � Title$ 	OProfTest��5   	   $
��auto��5   	   $
��2000��5   	   $
��1000��5   	    � Icon$  ../../Image/mainIcon64.ico��5   	   ��5      �� 5   
    �� ImageSource$ ../../Image/new.jpg��6   69   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,    � ColumnDefinitions 5   
   �� 5      $��75 �6   6,   �� 5      6!   �� 5      $��75 �6   6,   .+�5   
   �� -   5        $��0��5      $
��50px��6   $
��50px��6*   $��Center=�68   $��../../Image/backArrow.png��6U    � Cursor$
 Hand��6x   6�   �� 5    
   $��1��6   $��0��6$   $	��650��61   $
��0 10q�6=   $��28(�6K   $��SemiBold"�6Y    �� TextAlignment$ Center=�6o   $��Center=�6�   $��Center=�6�   ��6�   �� *	TestTitle+ �� UpdateSourceTrigger$ PropertyChanged=�6   �� 5!   
   $��2��6   $��1��6   $��Center=�6.   	 �� CornerRadius$	 10��6K   ���#E67E22$��2q�6s   $	��800��6�   $
��auto��6�   $��	0 15 0 15q�6�   ._�5"      �� 
�������
 J� Opacity$

 0.95Q�6,   .+�5#      �� $��
30 20 0 30q�6   $
��Wrap=�60   $��1��6D   $��2��6T   $	��745��6a   $��20(�6m   $��Medium"�6{   $	��Top=�6�   $
��Left=�6�   ��6�   �� *QuestionTitle+$ PropertyChanged=�6  �� 5$      $��1��6c   $��2��6s   $	��270��6�   $
��Left=�6�   $
��Auto��6�   $��25 10q�6�   ��6   �� *	Answers+ �� SelectedItem 6;   �� *SelectedAnswer+��5%      �� 5&      .R�5'      ��    AnswerLabelTemplate#��[  6>   $	��250��6k   $
��50px��6w   $
��18px(�6�   ��6$   �� *Title+6�   5(      ��5*      �� 5+      .R�5,      �� 5-      5/      50      51      �� 52   
   $��
25 10 0 20q�6   $��1��6(   $��3��68   $	��650��6E   $��22(�6Q   $��Medium"�6_   $��Bottom=�6s   $
��Left=�6�   ��6�   �� *State+$ PropertyChanged=�6�   53      54      