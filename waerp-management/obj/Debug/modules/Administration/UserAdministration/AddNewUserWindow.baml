   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null�Lclr-namespace:waerp_management.application.Administration.UserAdministration>waerp_management.application.Administration.UserAdministration  5      U    Owaerp_management.application.Administration.UserAdministration.AddNewUserWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^localLclr-namespace:waerp_management.application.Administration.UserAdministration     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        � Title$  RentSelectedItemView��5	      $	��900��5
      $	��900��5       � AllowsTransparency . 
������    MaterialDesignBody#���   5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��4* �6   �� 5      $
��0.5* �6   .+�5   
   �� $��3��5      $��2��5        BorderDialog#��[ 5      ._�5      �� $��0q�6    ! RectangleDialogAdministration#��[ 6#   5       �� 5"   
   $��0��5#      $��Center=�5$      $��18(�5%      $��DemiBold"�5&      $��Neuen Mitarbeiter anlegen��5'       �� TextAlignment$ Center=�5(      $
��Wrap=�5)      �� 5*   
   $��1��5+      $��Stretch=�5,      $��Center=�5-       �� Orientation$ Vertical=�5.      .+�5/      �� -   5         ��Name$ username��50      $��
20,20,20,0q�51      $��Stretch=�52      $��Center=�53      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Benutzername��54      	 �� AcceptsReturn	 .    MaterialDesignFilledTextBox#��[ 58      
 �� TextWrapping$

 Wrap=�59      {�56      ��  �� Path$ 	IsChecked� �� ElementName$0 *MaterialDesignFilledTextBoxEnabledComboBox��59      G� 5:      -   5         G�Name$ user_password��5;      $��20,20,20,20q�5<      $��Center=�5=      $ Passwort��5>       ) %MaterialDesignFilledRevealPasswordBox#��[ 5A      {�5?      �� $! RevealPasswordFilledEnabled��$ 	IsChecked�5A      �� 5D      -   5        $ 
user_ident��5E      $��
20,0,20,60q�5F      $��Stretch=�5G      $��Center=�5H      $' !Identifikationsnummer (4 stellig)��5I      	 .   �� 	MaxLength$ 4��5L      #��[ 5N      $

 Wrap=�5O      {�5K      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5O      �� 5P      -   5        $
 name��5Q      $��	20,0,20,0q�5R      $��Stretch=�5S      $��Center=�5T      $ Vorname��5U      	 .  #��[ 5Y      $

 Wrap=�5Z      {�5W      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5Z      �� 5[      -   5        $ surname��5\      $��
20,20,20,0q�5]      $��Stretch=�5^      $��Center=�5_      $ Nachname��5`      	 .  #��[ 5d      $

 Wrap=�5e      {�5b      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5e      �� 5f      -   5        $ email��5g      $��
20,20,20,0q�5h      $��Stretch=�5i      $��Center=�5j      $ E-Mail��5k      	 .  #��[ 5p      $

 Wrap=�5q      {�5m      �� $ 	IsChecked�$0 *MaterialDesignFilledTextBoxEnabledComboBox��5q      �� 5r      -   5         ��Name$ 	user_role��5s      $��
20,20,20,0q�5t      $��Stretch=�5u      $	��Top=�5v      $ Mitarbeiterrolle��5w       �� 
IsEditable .    MaterialDesignFilledComboBox#��[ 5z      {�5y      �� $ 	IsChecked�$# FilledComboBoxEnabledCheckBox��5z      #  MaterialDesignThemes.Wpf.Card  5{      $	��200��5|      $��
20,20,20,0q�5}      $��0q�5~      .��5      �  � ColumnDefinitions 5�      �� 5�      $��2* �6+   �� 5�      $��* �6+   �� 5�      $��2* �6+    5�      �� 5�      $��* �6(   �� 5�      $��5* �6(   .+�5�      �� $��0��5�      $��10,0,0,0q�5�      $��Center=�5�      $��16(�5�      $��Medium"�5�      $��Berechtigungen��5�      � 5�      $��1��6    5�      �� 5�      $
��0.8* �6,   �� 5�      $��6* �6,   .+�5�      �� $��0��5�      $��10,0,0,0q�5�      $��12(�5�      $��inaktive Rechte��5�      �� 5�      $��1��5�      $��
10,0,10,10q�5�      
�������$��3q�5�       �� CornerRadius$ 5��5�      ._�5�      �� -   5         ��Name$ unselectedPrivileges��5�   !   {�5�   !   �� *	IsChecked+$ EnableListBox��5�   !   5�      5�      �� 5�      $��1��5�      $��1��5�      $��50��5�      $��Center=�5�      $ Vertical=�5�      .+�5�      �� -	   5         ��Name$ addPrivileges��5�      $��1��5�      $��0,0,0,10q�5�       " MaterialDesignPaperLightButton#��[ 5�       � ToolTip$3 -Resource name: MaterialDesignPaperLightButton��5�      .��5�      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ArrowRightBold=�66   5�      �� 5�      -
   5        $ removePrivilege��5�      $��1��5�      $��	0,10,0,10q�5�      #��[ 5�      $3 -Resource name: MaterialDesignPaperLightButton��5�      {�5�      �� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL�.��5�        $ ArrowLeftBold=�66   5�      5�      � 5�      $��1��6   $��3��6(    5�      �� 5�      $
��0.8* �6,   �� 5�      $��6* �6,   .+�5�      �� $��0��5�      $��10,0,0,0q�5�      $��12(�5�      $��aktive Rechte��5�      �� 5�      $��1��5�      $��
10,0,10,10q�5�      
�������$��3q�5�      $ 5��5�      ._�5�      �� -   5        $ selectedPrivileges��5�   !   {�5�   !   �� *	IsChecked+$ EnableListBox��5�   !   5�      5�      5�      5�      5�      � 5�   
   $��2��6   $��Center=�6    5�      �� 5�      $��* �6#   �� 5�      $��* �6#   .+�5�      �� -   5        $ CloseDialog��5�      $��0��5�      $	��200��5�      $��20,0,0,0q�5�      $
��left=�5�      $��Center=�5�      $��	Abbrechen��5�        CancleButton#��[ 5�      �� 5�      -   5        $ 
CreateUser��5�      $��1��5�      $	��200��5�      $��0,0,20,0q�5�      $��Right=�5�      $��Center=�5�      $��Mitarbeiter anlegen��5�        NormalButton#��[ 5�      5�      5�      5�      