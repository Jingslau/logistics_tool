   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null�Nclr-namespace:waerp_management.application.Administration.VendorAdministration@waerp_management.application.Administration.VendorAdministration  5      W    Qwaerp_management.application.Administration.VendorAdministration.EditVendorWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      `localNclr-namespace:waerp_management.application.Administration.VendorAdministration     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � Title$  RentSelectedItemView��5	      $	��900��5
      $	��800��5       � AllowsTransparency . 
������    MaterialDesignBody#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��6* �6   �� 5      $��* �6   .+�5   
   �� $��3��5      $��2��5        BorderDialog#��[ 5      ._�5      �� $��0q�6    ! RectangleDialogAdministration#��[ 6#   5      �� 5!   
   $��0��5"      $��Center=�5#      $��18(�5$      $��DemiBold"�5%      $��Lieferant bearbeiten��5&       �� TextAlignment$ Center=�5'      $
��Wrap=�5(      �� 5)   
   $��1��5*      $��Stretch=�5+      $��Center=�5,       �� Orientation$ Vertical=�5-      .+�5/      �� -   5         ��Name$ 
VendorName��50      $��	20,0,20,0q�51      $��Stretch=�52      $��Center=�53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Firmenname��54      	 �� AcceptsReturn	 .    MaterialDesignFilledTextBox#��[ 57      
 �� TextWrapping$

 Wrap=�58      {�56      ��  �� Path$ 	IsChecked� �� ElementName$0 *MaterialDesignFilledTextBoxEnabledComboBox��58      �� 59      -   5        $ VendorAdress��5:      $��
20,20,20,0q�5;      $��Stretch=�5<      $��Center=�5=      $ Adresse��5>      	 .  #��[ 5A      $

 Wrap=�5B      {�5@      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5B      �� 5C      -   5        $ VendorPostcode��5D      $��
20,20,20,0q�5E      $��Stretch=�5F      $��Center=�5G      $	 PLZ��5H      	 .  #��[ 5K      $

 Wrap=�5L      {�5J      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5L      �� 5M      -   5        $ 
VendorCity��5N      $��
20,20,20,0q�5O      $��Stretch=�5P      $��Center=�5Q      $ Stadt��5R      	 .  #��[ 5U      $

 Wrap=�5V      {�5T      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5V      �� 5W      -   5        $ VendorCountry��5X      $��
20,20,20,0q�5Y      $��Stretch=�5Z      $��Center=�5[      $ Ländercode��5\      	 .  #��[ 5_      $

 Wrap=�5`      {�5^      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5`      �� 5a      -   5        $ VendorWebsite��5b      $��
20,20,20,0q�5c      $��Stretch=�5d      $��Center=�5e      $ Webseite��5f      	 .  #��[ 5i      $

 Wrap=�5j      {�5h      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5j      �� 5k      -   5        $ VendorPhone��5l      $��
20,20,20,0q�5m      $��Stretch=�5n      $��Center=�5o      $ Telefon��5p      	 .  #��[ 5s      $

 Wrap=�5t      {�5r      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5t      �� 5u      -   5        $ 
VendorMail��5v      $��
20,20,20,0q�5w      $��Stretch=�5x      $��Center=�5y      $ E-Mail��5z      	 .  #��[ 5}      $

 Wrap=�5~      {�5|      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5~      �� 5      -	   5        $ VendorContact��5�      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $ Ansprechpartner��5�      	 .  #��[ 5�      $

 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5�      5�      � 5�   
   $��2��5�      $��10q�5�      $��Stretch=�5�       � ColumnDefinitions 5�      �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� -
   5         ��Name$ CloseDialog��5�      $��0��5�      $	��200��5�      $��	10,30,0,0q�5�      $
��left=�5�      $��	Abbrechen��5�        CancleButton#��[ 5�      �� 5�      -   5        $ SaveChanges��5�      $��1��5�      $	��200��5�      $��	0,30,10,0q�5�      $��Right=�5�      $��Änderungen speichern��5�        NormalButton#��[ 5�      5�      5�      5�      