   M S B A M L   `   `   `  ����K  GDeweyDecimalGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DeweyDecimalGameDeweyDecimalGame  5   	   !    DeweyDecimalGame.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   *localclr-namespace:DeweyDecimalGame  5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5	   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5
   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Main Screen��5   	   $	��450��6   $	��760��6*    � 
ResizeMode$ CanMinimize=�66   .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF2C3B50��6    � Offset$ 1Q�61   � 5      $ 	#FF232526��6   5      .+�5   
   �� $
��Wrap=�6   $-��'Choose a game mode below to practice on��6(   	 �� TextAlignment$	 Center=�6W   $��0,70,0,0q�6n   $��32(�6�   
�������6�   �� 5   
   -   5        
 ��Name$
 	tbWelcome��5      $
��Wrap=�6'   $��Welcome��6;   $	 Center=�6J   $��0,25,0,0q�6a   $��18(�6s   
�������6�   �� 5   
   -   5         ��Name$ stckReplaceBooks��5   �   $
��Left=�6   $	��206��60   $��
30,150,0,0q�6=   $	��Top=�6Q   $	��197��6i   
����  �.+�5       �� -   5        $
 tbReplacingBooks��5       $
��Wrap=�62   $��Replacing Books��6F   $	 Center=�6]   $��0,90,0,0q�6t   $��20(�6�   
�������6�   5!      �� 5#   
   -   5        $ stckFindCallNumbers��5#   �   $
��Left=�6   $	��206��60   $��523,150,0,0q�6=   $	��Top=�6R   $	��197��6j   
����  �.+�5$      �� -   5        $
 tbFindingNumbers��5$      $
��Wrap=�62   $��Finding Call Numbers��6F   $	 Center=�6b   $��0,90,0,0q�6y   $��20(�6�   
�������6�   5%      �� 5'   
   -   5        $ stckIdentifyBooks��5'   �   $
��Left=�6   $	��206��60   $��278,150,0,0q�6=   $	��Top=�6R   $	��197��6j   
����  �.+�5(      �� -   5        $
 tbIdentifyingAreas��5(      $
��Wrap=�64   $��Identifying Areas��6H   $	 Center=�6a   $��0,90,0,0q�6x   $��20(�6�   
�������6�   5)      5+      5,      