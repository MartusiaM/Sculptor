   M S B A M L   `   `   `  ����C  ?Sculptor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SculptorSculptor  5   	       Sculptor.NewModelWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Sculptor     5   	     � Title$  Create new model��5   	   $	��220��6"   $	��300��6/    � 
ResizeMode$ NoResize=�5	   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   	    � Icon$# Images/art_sculpture_icon.jpg��6-   .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��WrapWithOverflow=�6   $��Center=�64   $��Center=�6O    �� TextAlignment$ Center=�6l   $��0��6�   $��2��6�   PNEnter the initial cube size. Each dimension must be within the range: 3-70 cm.6�   �� 5   
   $��0��6   $��1��6    $��	10,3,10,3q�6-   .��6@   Width:6H   �� 5   
   -   5        $��widthTextBox��5      $��1��6&   $��1��66   $��3q�6C   $��Center=�6N   .��6p   ��5      �� 5        �� Path$ NewWidth�6   	 �� UpdateSourceTrigger$	 PropertyChanged=�6*   
 �� ValidationRules
 5!      !   Sculptor.NewModelValidation  5"         MinValue$ 3Q�63      MaxValue$ 70Q�6@   5$      .��5%      5&      �� 5'   
   $��0��6   $��2��6    $��	10,3,10,3q�6-   .��6@   	Height:6I   �� 5(   
   -   5        $��heightTextBox��5(      $��1��6'   $��2��67   $��3q�6D   $��Center=�6O   .��6q   ��5)      �� 5*      $ 	NewHeight�6   $	 PropertyChanged=�6+   
 5+        5,      $ 3Q�63   $ 70Q�6@   5.      .��5/      50      �� 51   
   $��0��6   $��3��6    $��	10,3,10,3q�6-   .��6@   	Length:6I   �� 52   
   -   5        $��lengthTextBox��52      $��1��6'   $��3��67   $��3q�6D   $��Center=�6O   .��6q   ��53      �� 54      $ 	NewLength�6   $	 PropertyChanged=�6+   
 55        56      $ 3Q�63   $ 70Q�6@   58      .��59      5:      �� 5<   
   $��0��6   $��2��6%   $��4��69    �� Orientation$ 
Horizontal=�6F   $��Right=�6_   .+�5=      �� -   5        $��okButton��5=       �� 	IsDefault . $��5q�6M   $��70��6X   .��6c   OK6g   �� 5>      -   5        $��cancelButton��5>       �� IsCancel . $��5q�69   $��70��6D   .��6O   Cancel6W   5?      5A      5B      