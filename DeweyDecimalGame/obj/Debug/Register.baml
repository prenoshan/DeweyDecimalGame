   M S B A M L   `   `   `  ����K  GDeweyDecimalGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DeweyDecimalGameDeweyDecimalGame  5   	       DeweyDecimalGame.Register   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   *localclr-namespace:DeweyDecimalGame  5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5	   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5
   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Register��5   	   $	��600��6   $	��600��6'    � 
ResizeMode$ CanMinimize=�63   .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF2C3B50��6    � Offset$ 1Q�61   � 5      $ 	#FF232526��6   5      .+�5   
   �� $
��Left=�6   $	��571��60   $	��Top=�6=   $	��292��6U   ��5      �� 5      	 �� ImageSource$	 /res/firewatch.jpg��6   
 j� Stretch$
 UniformToFill=�6>   5       �� 5"   
   $
��Left=�6   $	��250��60   $	��Top=�6=   $	��270��6U   $��	10,10,0,0q�6a   ��5#      �� 5$      $	 /res/logo.jpg��6   $
 UniformToFill=�69   5&      �� 5(   
    �� TextAlignment$ Center=�5)         MaterialDesignBody1TextBlock#��[ 5*   	   $��28(�5+   	   $��297,26,10,461q�5,   	   $
��Wrap=�6    Register an account to play5.      �� 50   
   -   5         ��Name$ 
tbUsername��50      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username��51       % !MaterialDesignFloatingHintTextBox#��[ 52      $��320,134,32,373q�6N   $��20(�6f   G� 54   
   -   5         G�Name$ 
tbPassword��54      $ Password��55       ) %MaterialDesignFloatingHintPasswordBox#��[ 56      $��20(�58      $��320,214,32,293q�6$   �� 5:   
   -   5         ��Name$ btnReg��5:      $��Register��6!   $��24(�5;      $
��Left=�5<      $��339,327,0,0q�6,   $	��Top=�6A   $	��202��6Y   $��65��6e   �� 5>   
   -   5         ��Name$ 	goToLogin��5B   	   $ Center=�5?      $��	underline{�5A      #��[ 5C   	   $��20(�5D   	   $��343,410,49,125q�5E   	   
Or Login5G      )  #MaterialDesignThemes.Wpf.DialogHost  5J   
   -   5        $��userExistsDialog��5J   $     MaterialDesignDivider#���  6<      DialogContent 5L      �� 5N      $��16q�5O   	   .+�5Q      �� #��[ 5R   	   $��20(�5S   	   $��Center=�5T   
   $��16q�5U      -+Username already exists, please try another5W      �� 5Y        MaterialDesignFlatButton#��[ 5Z       �� IsCancel .    CloseDialogCommand#��Z 5\      $��Center=�5]      .��6(   Ok5_      5a      5e        5g   
   -   5        $��userCreatedDialog��5g   $   #���  6=    5i      �� 5k      $��16q�5l   	   .+�5n      �� #��[ 5o   	   $��20(�5p   	   $��Center=�5q   
   $��16q�5r      User created successfully5t      �� 5v      #��[ 5w       . #��Z 5y      $��Center=�5z      .��6(   Ok5|      5~      5�      5�      5�      