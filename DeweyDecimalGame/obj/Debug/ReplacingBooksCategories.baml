   M S B A M L   `   `   `  ����K  GDeweyDecimalGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DeweyDecimalGameDeweyDecimalGame  5   	   /    )DeweyDecimalGame.ReplacingBooksCategories   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   *localclr-namespace:DeweyDecimalGame  5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5	   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5
   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Book Categories��5   	   $	��600��6!   $	��900��6.    � 
ResizeMode$ CanMinimize=�6:   .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF2C3B50��6    � Offset$ 1Q�61   � 5      $ 	#FF232526��6   5      .+�5   
   �� $
��Wrap=�6   $[��UChoose a category below and you will be able to practice within those book categories��6(   	 �� TextAlignment$	 Center=�6�   $��0,40,0,0q�6�   $��32(�6�   
�������6�   �� 5   
   -   5        $
��Left=�5   J   $	��150��6e   $��
15,172,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5      �� -   5        
 ��Name$
 tbGeneralKnowledge��5   A   $	 Center=�6   $��0,60,0,0q�6/   $
��Wrap=�6]   $��000
General Knowledge��6q   $��18(�6�   5       �� 5"   
   -   5        $
��Left=�5"   J   $	��150��6e   $��192,172,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5#      �� -   5        $
 tbPhisandPsy��5#      $��0,60,0,0q�6.   $
��Wrap=�6A   $#��100
Philosophy and Psychology��6U   $	 Center=�6~   $��18(�6�   5$      �� 5&   
   -   5        $
��Left=�5&   J   $	��150��6e   $��369,172,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5'      �� -   5        $
 
tbReligion��5'      $��0,60,0,0q�6,   $
��Wrap=�6?   $��200
Religion��6S   $	 Center=�6k   $��18(�6�   5(      �� 5*   
   -   5        $
��Left=�5*   J   $	��150��6e   $��546,172,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5+      �� -   5        $
 tbSocialSciences��5+      $��0,60,0,0q�62   $
��Wrap=�6E   $��300
Social Sciences��6Y   $	 Center=�6x   $��18(�6�   5,      �� 5.   
   -	   5        $
��Left=�5.   J   $	��150��6e   $��722,172,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5/      �� -
   5        $
 tbLanguages��5/      $��0,60,0,0q�6-   $
��Wrap=�6@   $��400
Languages��6T   $	 Center=�6m   $��18(�6�   50      �� 52   
   -   5        $
��Left=�52   J   $	��150��6e   $��
15,352,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�53      �� -   5        $
 	tbScience��53      $��0,60,0,0q�6+   $
��Wrap=�6>   $��500
Science��6R   $	 Center=�6i   $��18(�6�   54      �� 56   
   -   5        $
��Left=�56   J   $	��150��6e   $��192,352,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�57      �� -   5        $
 tbTech��57      $��0,60,0,0q�6(   $
��Wrap=�6;   $��600
Technology��6O   $	 Center=�6i   $��18(�6�   58      �� 5:   
   -   5        $
��Left=�5:   J   $	��150��6e   $��369,352,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5;      �� -   5        $
 tbArts��5;      $��0,60,0,0q�6(   $
��Wrap=�6;   $��700
Arts and Recreation��6O   $	 Center=�6r   $��18(�6�   5<      �� 5>   
   -   5        $
��Left=�5>   J   $	��150��6e   $��546,352,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5?      �� -   5        $
 tbLit��5?      $��0,60,0,0q�6'   $
��Wrap=�6:   $��800
Literature��6N   $	 Center=�6h   $��18(�6   5@      �� 5B   
   -   5        $
��Left=�5B   J   $	��150��6e   $��722,352,0,0q�6r   $	��Top=�6�   $	��150��6�   
����  �.+�5C      �� -   5        $
 tbHist��5C      $��0,60,0,0q�6(   $
��Wrap=�6;   $��900
History and Geography��6O   $	 Center=�6t   $��18(�6�   5D      5F      5G      