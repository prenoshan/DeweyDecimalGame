   M S B A M L   `   `   `  ����K  GDeweyDecimalGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DeweyDecimalGameDeweyDecimalGame  5   	       DeweyDecimalGame.Login   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   *localclr-namespace:DeweyDecimalGame  5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5	   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5
   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Login��5   	   $	��600��6   $	��600��6$    � 
ResizeMode$ CanMinimize=�60   .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF2C3B50��6    � Offset$ 1Q�61   � 5      $ 	#FF232526��6   5      .+�5   
   �� $
��Left=�6   $	��571��60   $	��Top=�6=   $	��292��6U   ��5      �� 5      	 �� ImageSource$	 /res/firewatch.jpg��6   
 j� Stretch$
 UniformToFill=�6>   5       �� 5#   
   $
��Left=�6   $	��250��60   $	��Top=�6=   $	��270��6U   $��	10,10,0,0q�6a   ��5$      �� 5%      $	 /res/logo.jpg��6   $
 UniformToFill=�69   5'      �� 5)   
    �� TextAlignment$ Center=�5*         MaterialDesignBody1TextBlock#��[ 5+   	   $��28(�5,   	   $��297,26,10,461q�5-   	   $
��Wrap=�6    Login to your gaming profile5/      �� 51   
   -   5         ��Name$ 
tbUsername��51      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username��52       % !MaterialDesignFloatingHintTextBox#��[ 53      $��320,134,32,373q�6N   $��20(�6f   G� 55   
   -   5         G�Name$ 
tbPassword��55      $ Password��56       ) %MaterialDesignFloatingHintPasswordBox#��[ 57      $��20(�59      $��320,214,32,293q�6$   �� 5;   
   -   5         ��Name$ btnLogin��5;      $��Login��6#   $��24(�5=      $
��Left=�5>      $��339,327,0,0q�6,   $	��Top=�6A   $	��202��6Y   $��65��6e   �� 5@   
   -   5         ��Name$ goToRegister��5D   	   $ Center=�5A      $��	underline{�5C      #��[ 5E   	   $��20(�5F   	   $��343,410,49,125q�5G   	   Or Register5I      )  #MaterialDesignThemes.Wpf.DialogHost  5L   
   -   5        $��invalidDetailsDialog��5L   $     MaterialDesignDivider#���  6@      DialogContent 5N      �� 5P      $��16q�5Q   	   .+�5S      �� #��[ 5T   	   $��20(�5U   	   $��Center=�5V   
   $��16q�5W      Invalid details, try again5Y      �� 5[        MaterialDesignFlatButton#��[ 5\       �� IsCancel .    CloseDialogCommand#��Z 5^      $��Center=�5_      .��6(   Ok5a      5c      5g      5i      5j      