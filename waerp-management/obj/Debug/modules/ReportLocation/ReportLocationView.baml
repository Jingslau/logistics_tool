   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=nulli9clr-namespace:waerp_management.application.ReportLocation+waerp_management.application.ReportLocation  5      D    >waerp_management.application.ReportLocation.ReportLocationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Klocal9clr-namespace:waerp_management.application.ReportLocation     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � Title$  RentSelectedItemView��5	      $	��900��5
      $	��500��5       � AllowsTransparency . 
������    MaterialDesignBody#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��4* �6   �� 5      $��* �6   .+�5   
   �� $��3��5      $��2��5      ���#FF5252$��4q�5       �� CornerRadius$ 10��5      ._�5       �� $��0q�5!      ���#EEEEEE �� RadiusX$ 9��5#       �� RadiusY$ 9��5$      ���#FF52525%      5&      �� 5(   
   $��0��5)      $��Center=�5*      $��24(�5+      $��DemiBold"�5,      $��Fehlbestand melden��5-       �� TextAlignment$ Center=�5.      $
��Wrap=�5/      �� 50   
   $��1��51      $��Stretch=�52      	 �� Orientation$	 Vertical=�53      .+�54      �� -   5        
 ��Name$
 	ItemIdent��55      $��	20,0,20,0q�56      $��Stretch=�57      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Artikelnummer��58       �� AcceptsReturn .  {�.    MaterialDesignFilledTextBox#��[ 5;       �� TextWrapping$
 Wrap=�5<      �� 5=      -   5        $
 LocationIdent��5>      $��
20,20,20,0q�5?      $��Stretch=�5@      $ Fehlerhafter Lagerort��5A       .  {�.  #��[ 5D      $
 Wrap=�5E      �� 5F      $��20,20,20,10q�5G      $��13(�5H      $��DemiBold"�5I      $��Beschreibung��5J      �� 5K      -   5        $
 Description��5L      $	��200��5M      $��	20,0,20,0q�5N      $��Stretch=�5O      $	��Top=�5P      $ Fehlerbeschreibung��5Q       .  �� 	MaxLength$	 500��5S       �� 	IsEnabled . $�� ��5U      $
 Wrap=�5V      " �� VerticalScrollBarVisibility$
 Auto=�5W      5Y      � 5Z   
   $��2��5[      $��	20,0,20,0q�5\      $��Stretch=�5]      $	��Top=�5^       � ColumnDefinitions 5_      �� 5`      $��* �6#   �� 5a      $��* �6#   .+�5c      �� -   5         ��Name$ CloseDialog��5d      $��0��5e      $	��200��5f      $��	10,30,0,0q�5g      $
��left=�5h      $	��Top=�5i      $��	Abbrechen��5k        CancleButton#��[ 5l      �� 5m      -   5        $ SaveChanges��5n      $��1��5o      $	��200��5p      $��	0,30,10,0q�5q      $��Right=�5r      $	��Top=�5s      $��Meldung senden��5u        NormalButton#��[ 5v      5z      5|      5}      