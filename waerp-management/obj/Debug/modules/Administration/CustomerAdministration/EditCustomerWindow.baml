   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=nullo<clr-namespace:waerp_management.ValidationRulesSet.ViewModels.waerp_management.ValidationRulesSet.ViewModels  5      Y1clr-namespace:waerp_management.ValidationRulesSet#waerp_management.ValidationRulesSet  5	      [    Uwaerp_management.application.Administration.CustomerAdministration.EditCustomerWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Nlocal<clr-namespace:waerp_management.ValidationRulesSet.ViewModels     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      B
validation1clr-namespace:waerp_management.ValidationRulesSet  5	        � Title$  RentSelectedItemView��5
      $	��900��5      $	��800��5       � AllowsTransparency . 
������    MaterialDesignBody#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5       � DataContext 5      I  Cwaerp_management.ValidationRulesSet.ViewModels.NewCustomerViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��6* �6   �� 5      $��* �6   .+�5   
   �� $��3��5      $��2��5         BorderDialog#��[ 5!      ._�5"      �� $��0q�6    ! RectangleDialogAdministration#��[ 6#   5#      �� 5%   
   $��0��5&      $��Center=�5'      $��18(�5(      $��DemiBold"�5)      $��Kunden bearbeiten��5*       �� TextAlignment$ Center=�5+      $
��Wrap=�5,      �� 5-   
   $��1��5.      $��Stretch=�5/      $��Center=�50       �� Orientation$ Vertical=�51      .+�52      �� -   5         ��Name$ VendorNumber��53      $��	20,0,20,0q�54      $��Stretch=�55      $��Center=�56      )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Kundennummer��57      
 �� AcceptsReturn
 .   �� 	MaxLength$ 10��5:        MaterialDesignFilledTextBox#��[ 5<       �� TextWrapping$
 Wrap=�5=      {�59      ��  �� Path$ 	IsChecked� �� ElementName$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5=   %   ��5>      �� 5?      $ 
CustomerID�6    �� UpdateSourceTrigger$ PropertyChanged=�60    �� ValidationRules 5@      =   7waerp_management.ValidationRulesSet.TextFieldValidation  5A      5C      .��5D       5E      �� 5G      -   5        $ CustomerName��5H      $��
20,20,20,0q�5I      $��Stretch=�5J      $��Center=�5K      $	 
Firmenname��5L      
 .  $ 50��5O      #��[ 5P      $
 Wrap=�5Q      {�5N      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5Q   %   ��5R      �� 5S      $ CustomerName�6   $ PropertyChanged=�62    5T        5U      5W      .��5X       5Y      �� 5Z      -   5        $ CustomerAdress��5[      $��
20,20,20,0q�5\      $��Stretch=�5]      $��Center=�5^      $	 Adresse��5_      
 .  $ 50��5b      #��[ 5c      $
 Wrap=�5d      {�5a      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5d   %   ��5e      �� 5f      $ CustomerAddress�6   $ PropertyChanged=�65    5g        5h      5j      .��5k       5l      �� 5m      -   5        $ CustomerPostcode��5n      $��
20,20,20,0q�5o      $��Stretch=�5p      $��Center=�5q      $		 PLZ��5r      
 .  $ 5��5u      #��[ 5w      $
 Wrap=�5x      {�5t      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5x   %   ��5y      �� 5z      $ CustomerZIP�6   $ PropertyChanged=�61    5{      <   6waerp_management.ValidationRulesSet.PostcodeValidation  5|      5~      .��5       5�      �� 5�      -   5        $ CustomerCity��5�      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $	 Stadt��5�      
 .  $ 50��5�      #��[ 5�      $
 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5�   %   ��5�      �� 5�      $ CustomerCity�6   $ PropertyChanged=�62    5�        5�      5�      .��5�       5�      �� 5�      -   5        $ CustomerCountry��5�      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $	 Ländercode��5�      
 .  $ 3��5�      #��[ 5�      $
 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5�   %   ��5�      �� 5�      $ CustomerCountry�6   $ PropertyChanged=�65    5�      ?  9waerp_management.ValidationRulesSet.CountryCodeValidation  5�      5�      .��5�       5�      �� 5�      -   5        $ CustomerWebsite��5�      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $	 Webseite��5�      
 .  $	 100��5�      #��[ 5�      $
 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5�   %   ��5�      �� 5�      $ CustomerWebsite�6   $ PropertyChanged=�65    5�        5�      5�      .��5�       5�      �� 5�      -   5        $ CustomerPhone��5�      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $	 Telefon (+ -> 00)��5�      
 .  $ 20��5�      #��[ 5�      $
 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5�   %   ��5�      �� 5�      $ CustomerPhone�6   $ PropertyChanged=�63    5�      ?   9waerp_management.ValidationRulesSet.PhonenumberValidation  5�      5�      .��5�       5�      �� 5�      -	   5        $ CustomerMail��5�      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $	 E-Mail��5�      
 .  $	 100��5�      #��[ 5�      $
 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5�   %   ��5�      �� 5�      $ CustomerMail�6   $ PropertyChanged=�62    5�      9   3waerp_management.ValidationRulesSet.EMailValidation  5�      5�      .��5�       5�      �� 5�      -
   5        $ CustomerContact��5�      $��
20,20,20,0q�5�      $��Stretch=�5�      $��Center=�5�      $	 Ansprechpartner��5�      
 .  $	 100��5�      #��[ 5�      $
 Wrap=�5�      {�5�      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��.��5�   %   ��5�      �� 5�      $ CustomerContactPerson�6   $ PropertyChanged=�6;    5�        5�      5�      .��5�       5�      5�      � 5�   
   $��2��5�      $��10q�5�      $��Stretch=�5�       � ColumnDefinitions 5�      �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� -   5         ��Name$ CloseDialog��5�      $��0��5      $	��200��5     $��	10,30,0,0q�5     $
��left=�5     $��	Abbrechen��5       CancleButton#��[ 5     �� 5     -   5        $ SaveChanges��5     $��1��5	     $	��200��5
     $��	0,30,10,0q�5     $��Right=�5     $��Änderungen speichern��5       NormalButton#��[ 5     5     5     5     