   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null�Nclr-namespace:waerp_management.application.Administration.VendorAdministration@waerp_management.application.Administration.VendorAdministration  5      Y    Swaerp_management.application.Administration.VendorAdministration.AddNewVendorWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      `localNclr-namespace:waerp_management.application.Administration.VendorAdministration     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � Title$  RentSelectedItemView��5	      $	��900��5
      $	��800��5       � AllowsTransparency . 
������    MaterialDesignBody#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��6* �6   �� 5      $��* �6   .+�5   
   �� $��3��5      $��2��5        BorderDialog#��[ 5      ._�5      �� $��0q�6    ! RectangleDialogAdministration#��[ 6#   5      �� 5!   
   $��0��5"      $��Center=�5#      $��18(�5$      $��DemiBold"�5%      $��Neuen Lieferanten anlegen��5&       �� TextAlignment$ Center=�5'      $
��Wrap=�5(      �� 5)   
   $��1��5*      $��Stretch=�5+      $��Center=�5,       �� Orientation$ Vertical=�5-      .+�5.      �� -   5         ��Name$ 
VendorName��5/      $��	20,0,20,0q�50      $��Stretch=�51      $��Center=�52      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Firmenname��53      	 �� AcceptsReturn	 .    MaterialDesignFilledTextBox#��[ 56      
 �� TextWrapping$

 Wrap=�57      {�55      ��  �� Path$ 	IsChecked� �� ElementName$0 *MaterialDesignFilledTextBoxEnabledComboBox��57      �� 58      -   5        $ VendorAdress��59      $��
20,20,20,0q�5:      $��Stretch=�5;      $��Center=�5<      $ Adresse��5=      	 .  #��[ 5@      $

 Wrap=�5A      {�5?      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5A      �� 5B      -   5        $ VendorPostcode��5C      $��
20,20,20,0q�5D      $��Stretch=�5E      $��Center=�5F      $	 PLZ��5G      	 .  #��[ 5J      $

 Wrap=�5K      {�5I      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5K      �� 5L      -   5        $ 
VendorCity��5M      $��
20,20,20,0q�5N      $��Stretch=�5O      $��Center=�5P      $ Stadt��5Q      	 .  #��[ 5T      $

 Wrap=�5U      {�5S      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5U      �� 5V      -   5        $ VendorCountry��5W      $��
20,20,20,0q�5X      $��Stretch=�5Y      $��Center=�5Z      $ Ländercode��5[      	 .  #��[ 5^      $

 Wrap=�5_      {�5]      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5_      �� 5`      -   5        $ VendorWebsite��5a      $��
20,20,20,0q�5b      $��Stretch=�5c      $��Center=�5d      $ Webseite��5e      	 .  #��[ 5h      $

 Wrap=�5i      {�5g      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5i      �� 5j      -   5        $ VendorPhone��5k      $��
20,20,20,0q�5l      $��Stretch=�5m      $��Center=�5n      $ Telefon��5o      	 .  #��[ 5r      $

 Wrap=�5s      {�5q      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5s      �� 5t      -   5        $ 
VendorMail��5u      $��
20,20,20,0q�5v      $��Stretch=�5w      $��Center=�5x      $ E-Mail��5y      	 .  #��[ 5|      $

 Wrap=�5}      {�5{      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5}      �� 5~      -	   5        $ VendorContact��5      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $ Ansprechpartner��5�      	 .  #��[ 5�      $

 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5�      5�      � 5�   
   $��2��5�      $��10q�5�      $��Stretch=�5�       � ColumnDefinitions 5�      �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� -
   5         ��Name$ CloseDialog��5�      $��0��5�      $	��200��5�      $��	10,30,0,0q�5�      $
��left=�5�      $��	Abbrechen��5�        CancleButton#��[ 5�      �� 5�      -   5        $ CreateVendor��5�      $��1��5�      $	��200��5�      $��	0,30,10,0q�5�      $��Right=�5�      $��Lieferanten anlegen��5�        NormalButton#��[ 5�      5�      5�      5�      