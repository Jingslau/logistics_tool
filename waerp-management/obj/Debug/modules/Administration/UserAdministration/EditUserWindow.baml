   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null�Lclr-namespace:waerp_management.application.Administration.UserAdministration>waerp_management.application.Administration.UserAdministration  5      S    Mwaerp_management.application.Administration.UserAdministration.EditUserWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^localLclr-namespace:waerp_management.application.Administration.UserAdministration     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � Title$  RentSelectedItemView��5	      $	��900��5
      $	��900��5       � AllowsTransparency . 
������    MaterialDesignBody#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��4* �6   �� 5      $
��0.5* �6   .+�5   
   �� $��3��5      $��2��5        BorderDialog#��[ 5      ._�5      �� $��0q�6    ! RectangleDialogAdministration#��[ 6#   5      �� 5!   
   $��0��5"      $��Center=�5#      $��18(�5$      $��DemiBold"�5%      $��Mitarbeiter bearbeiten��5&       �� TextAlignment$ Center=�5'      $
��Wrap=�5(      �� 5)   
   $��1��5*      $��Stretch=�5+      $��Center=�5,       �� Orientation$ Vertical=�5-      .+�5.      �� -   5         ��Name$ username��5/      $��
20,20,20,0q�50      $��Stretch=�51      $��Center=�52      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Benutzername��53      	 �� AcceptsReturn	 .    MaterialDesignFilledTextBox#��[ 56      
 �� TextWrapping$

 Wrap=�57      {�55      ��  �� Path$ 	IsChecked� �� ElementName$0 *MaterialDesignFilledTextBoxEnabledComboBox��57      G� 58      -   5         G�Name$ user_password��59      $��20,20,20,20q�5:      $��Center=�5;      $ Passwort��5<       ) %MaterialDesignFilledRevealPasswordBox#��[ 5>      {�5=      �� $! RevealPasswordFilledEnabled��$ 	IsChecked�5>      �� 5A      -   5        $ 
user_ident��5B      $��
20,0,20,60q�5C      $��Stretch=�5D      $��Center=�5E      $' !Identifikationsnummer (4 stellig)��5F      	 .   �� 	MaxLength$ 4��5I      #��[ 5K      $

 Wrap=�5L      {�5H      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5L      �� 5M      -   5        $
 name��5N      $��	20,0,20,0q�5O      $��Stretch=�5P      $��Center=�5Q      $ Vorname��5R      	 .  #��[ 5V      $

 Wrap=�5W      {�5T      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5W      �� 5X      -   5        $ surname��5Y      $��
20,20,20,0q�5Z      $��Stretch=�5[      $��Center=�5\      $ Nachname��5]      	 .  #��[ 5a      $

 Wrap=�5b      {�5_      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5b      �� 5c      -   5        $ email��5d      $��
20,20,20,0q�5e      $��Stretch=�5f      $��Center=�5g      $ E-Mail��5h      	 .  #��[ 5m      $

 Wrap=�5n      {�5j      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5n      �� 5o      -   5         ��Name$ 	user_role��5p      $��
20,20,20,0q�5q      $��Stretch=�5r      $	��Top=�5s      $ Berechtigung��5t       �� 
IsEditable .    MaterialDesignFilledComboBox#��[ 5w      {�5v      �� $ 	IsChecked�$# FilledComboBoxEnabledCheckBox��5w      #  MaterialDesignThemes.Wpf.Card  5x      $	��200��5y      $��
20,20,20,0q�5z      $��0q�5{      .��5|      �  � ColumnDefinitions 5}      �� 5~      $��2* �6+   �� 5      $��* �6+   �� 5�      $��2* �6+    5�      �� 5�      $��* �6(   �� 5�      $��5* �6(   .+�5�      �� $��0��5�      $��10,0,0,0q�5�      $��Center=�5�      $��16(�5�      $��Medium"�5�      $��Berechtigungen��5�      �� 5�      $��1��5�      $��10q�5�      
�������$��3q�5�       �� CornerRadius$ 5��5�      ._�5�      �� -   5         ��Name$ unselectedPrivileges��5�      {�5�      �� *	IsChecked+$ EnableListBox��5�      5�      �� 5�      $��1��5�      $��1��5�      $��50��5�      $��Center=�5�      $ Vertical=�5�      .+�5�      �� -	   5         ��Name$ addPrivileges��5�      $��1��5�      $��0,0,0,10q�5�       " MaterialDesignPaperLightButton#��[ 5�       � ToolTip$3 -Resource name: MaterialDesignPaperLightButton��5�      .��5�      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ArrowRightBold=�66   5�      �� 5�      -
   5        $ removePrivilege��5�      $��1��5�      $��	0,10,0,10q�5�      #��[ 5�      $3 -Resource name: MaterialDesignPaperLightButton��5�      {�5�      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL�.��5�        $ ArrowLeftBold=�66   5�      5�      �� 5�      $��1��5�      $��2��5�      $��10q�5�      
�������$��3q�5�      $ 5��5�      ._�5�      �� -   5        $ selectedPrivileges��5�      {�5�      �� *	IsChecked+$ EnableListBox��5�      5�      5�      5�      5�      � 5�   
   $��2��5�      $��10q�5�      $��Stretch=�5�       5�      �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� -   5        $ CloseDialog��5�      $��0��5�      $	��200��5�      $��20,0,0,0q�5�      $
��left=�5�      $��	Abbrechen��5�        CancleButton#��[ 5�      �� 5�      -   5        $ EditUser��5�      $��1��5�      $	��200��5�      $��0,0,20,0q�5�      $��Right=�5�      $��Änderungen speichern��5�        NormalButton#��[ 5�      5�      5�      5�      