   M S B A M L   `   `   `  ����I  ESnakesNLadders, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SnakesNLaddersSnakesNLadders  5   	       SnakesNLadders.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:SnakesNLadders     5   	     � Title$  Snakes and Ladders��5   	   $	��800��6$   $	��800��61   .��5
      �� -   5        $��MyCanvas��5
      $
|�True��6   .+�5   
   �� -   5        $��
startPanel��5      .+�5      �� $��150,200,150,50q�6   $,��&Enter the number of snakes and ladders��60   $��20(�6_   $��Miriam Fixed)�6m   $��DemiBold"�6�   �� 5       �� Orientation$ 
Horizontal=�6   $��80,0,-20,00q�62   .+�5      �� $��20(�6   $��Narkisim)�6*   $
��Bold"�6@   	Snakes �� 5      -   5         ��Name$ snakesInput��5      $	��200��63   6A   5      �� 5      $��20(�6   $��Narkisim)�6*   $
��Bold"�6@    �� Padding$ 	20 0 20 0q�6R   
Ladders �� 5      -   5        $ laddersInput��5      $	��200��64   6B   5      �� 5      -   5        $
��Play��5      
��� � �
�������$��20(�6k   $��Comic Sans MS)�6y    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 12Q�6+      Color$
 Gray��6;      	Direction$	 270Q�6H      Opacity$ .8Q�6X   	   ShadowDepth$	 3Q�6e   5      5      �� 5      -   5        
 ��Name$
 greetingOutput��5      5      � 5   
   -   5        $��gameGrid��5       �� Left$ 70��6   
 �� Top$	 500��60   $y�Hidden=�6A   
��� � � � RowDefinitions 5      �� 5      $��* �6     � ColumnDefinitions 5       �� 5!      $��* �6#   �� 5"      $��* �6#   �� 5#      $��* �6#   �� 5$      $��* �6#   �� 5%      $��* �6#   �� 5&      $��* �6#   �� 5'      $��* �6#   �� 5(      $��* �6#   �� 5)      $��* �6#   �� 5*      $��* �6#   5,      �� 5.   
   $��images/player1.png��6   $��30��6,   $ 70��67   $	 700��6H   �� 5/   
   $��images/player2.png��6   $��30��6,   $	 500��67   $	 700��6I   �� 50   
   -   5        $��player1Text��50      $��18(�6#   $��Narkisim)�61   $
��Bold"�6G   $�� ��6Y   $	 100��6d   $	 700��6v   �� 51   
   -	   5        $��player1PositionText��51      $��18(�6+   $��Narkisim)�69   $
��Bold"�6O   $��Player 1 position: 0��6a   $	 100��6�   $	 730��6�   �� 52   
   -
   5        $��player2Text��52      $��18(�6#   $��Narkisim)�61   $
��Bold"�6G   $�� ��6Y   $	 532��6d   $	 700��6v   �� 53   
   -   5        $��player2PositionText��53      $��18(�6+   $��Narkisim)�69   $
��Bold"�6O   $��Player 2 position: 0��6a   $	 532��6�   $	 730��6�   �� 55   
   -   5        $��	dicePanel��55      $	 350��6&   $	 700��68   $ 
Horizontal=�6I   $y�Hidden=�6b   .+�56      �� -   5        $��
dice1Image��56      $��30��6&   $	 500��61   $	 700��6C   �� 57      -   5        $��
dice2Image��57      $��30��6&   $	 500��61   $	 700��6C   $
��15 0q�6T   �� 58      $ Vertical=�6   .+�59      �� -   5        $��rollBtn��59      $
��Roll��6(   
����ح�$��5 0 0 0q�6_   6r   �� 5:      -   5        $��
diceResult��5:      $��16(�6*   $
��Bold"�68   
����  �$�� ��6]   $	 100��6h   $	 700��6z   5;      5<      5=      5>      