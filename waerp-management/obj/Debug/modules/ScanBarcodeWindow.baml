   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=nullK*clr-namespace:waerp_management.applicationwaerp_management.application  5      4    .waerp_management.application.ScanBarcodeWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:waerp_management.application     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � Title$  ScanBarcodeWindow��5	      $	��400��5
      $	��250��5       � AllowsTransparency . 
������    MaterialDesignBody#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��4��5      $��2��5      
�������$��1q�5       �� CornerRadius$ 10��5      ._�5      �� $��0q�5       ���#EEEEEE �� RadiusX$ 9��5"       �� RadiusY$ 9��5#      
�������5$      5%      � 5&   
   $��0��6   $��Stretch=�6   .+�5'      �� $��Stretch=�5(      
�������$ 5��5*      $ 5��5+      �� 5,      $��Center=�6    �� Orientation$ 
Horizontal=�66   .+�5-      '  !MaterialDesignThemes.Wpf.PackIcon  $��0,0,5,0q�5.      $��Center=�5/      $��Center=�50      
�������	   Kind$	 BarcodeScan=�52      �� 53      $��Center=�54      $��Center=�55      
�������$
��Wrap=�57      Artikelsuche59      5:      5=      �� 5>   
   $��1��5?      $��10q�5@      $��Center=�5A      $ 
Horizontal=�5B      .+�5C      �� -   5        
 ��Name$
 ItemIdentInput��5D      $	��300��5E      $��Stretch=�5F      $��Center=�5G      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Artikelnummer��5H       �� AcceptsReturn .    MaterialDesignFilledTextBox#��[ 5K       �� TextWrapping$
 Wrap=�5L      {�5J      ��  �� Path$ 	IsChecked� �� ElementName$0 *MaterialDesignFilledTextBoxEnabledComboBox��5L      �� 5M      -   5         ��Name$ ItemIdentInputBtn��5N      $��10q�5O        RoundButtons#��[ 5R       � ToolTip$
 Icon��5S      ��5Q      0  *MaterialDesignThemes.Wpf.PackIconExtension  *
Keyboard+5S      5T      �� 5V   
   $��2��5W      $��Center=�5X      $ 
Horizontal=�5Y      .+�5Z      �� -   5        $��3��5[      $	��120��5\      $��	50,0,50,0q�5]      $��Stretch=�5^      $��	Abbrechen��5`        CancleButton#��[ 5a      �� 5b      -   5        $��2��5c      $	��120��5d      $��	50,0,50,0q�5e      $��Stretch=�5f      $��Suchen��5h       �� 	IsDefault . {�.   NormalButton#��[ 5k      5m      5o      5p      