   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=nulli9clr-namespace:waerp_management.config.AccountSettingsView+waerp_management.config.AccountSettingsView  5      O,clr-namespace:waerp_management.errorHandlingwaerp_management.errorHandling  5	      G    Awaerp_management.config.AccountSettingsView.AccountSettingsWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Klocal9clr-namespace:waerp_management.config.AccountSettingsView     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      8rules,clr-namespace:waerp_management.errorHandling  5	      $
��1100��5
      $	��700��5        � AllowsTransparency  . 
������    MaterialDesignFont#���   5       � 
ResizeMode$ NoResize=�5      $��13(�5      $��Regular"�5        MaterialDesignBody#���  5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5       � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.7* �6   �� 5      $��5* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5!   
   �� $��2��5"      ���#8C8C8C$��10q�5$       �� CornerRadius$ 10��5%      ._�5&      �� ���#EEEEEE	 �� RadiusX$	 5��5(      
 �� RadiusY$
 5��5)      5*      � 5+   
   $��
10,10,10,0q�6   
������  5,      �� 5-      $��* �6#   �� 5.      $��* �6#   .+�50      �� $��2��51      ���#D0D0D0$	 5��53      $
 5��54      �� 55      $��2��56      $��0,10,0,0q�57      ���#D0D0D058      �� 59      $��0��5:      $��20,0,0,0q�5;      $��Center=�5<      $��18(�5=      $��DemiBold"�5>      $��Benutzereinstellungen��5?      �� 5@      $��1��5A      $��Right=�5B       �� Orientation$ 
Horizontal=�5C      .+�5D      �� -   5         ��Name$ CloseAccountSettings��5E      $��1��5F      $	��100��5G      $��0,0,20,0q�5H      $��Right=�5I      $��Center=�5J      $��
Schließen��5L        CancleButton#��[ 5M      5X      5[      #  MaterialDesignThemes.Wpf.Card  5_   
   $��1��5`      $��0q�5a      $��Stretch=�5b      $��Stretch=�5c      
������ .��5e      �� $��
10,0,10,10q�5f      ���#D0D0D0 " MaterialDesignFilledTabControl#��[ 5h       �� TabStripPlacement$
 Left=�5i      .@�5k      �� 
������� � Cursor$
 Hand��5m      $��Benutzerkonto��5n      .��5o      �  5p      �� 5q      $��* �6/   �� 5r      $��* �6/   .+�5t      �� $��
20,50,20,0q�5u      $��Center=�5v      $ Vertical=�5w      .+�5x      �� $��0,20,0,0q�6)   $ Vertical=�6;   .+�5y   "   �� -   5         ��Name$ UserID��5z   %   $��24(�5{   %   
���   �$
��0815��5}   %   �� 5~   "   $��18(�5   %   $��Light"�5�   %   
���   �$��MitarbeiterIdent.-Nr.��5�   %   5�      �� 5�      $��0,20,0,0q�6)   $ Vertical=�6;   .+�5�   "   �� -   5        $ vname��5�   %   $��24(�5�   %   
���   �$	��Max��5�   %   �� 5�   "   $��18(�5�   %   $��Light"�5�   %   
���   �$��Vorname��5�   %   5�      �� 5�      $��0,20,0,0q�6)   $ Vertical=�6;   .+�5�   "   �� -   5        $ Surname��5�   %   $��24(�5�   %   
���   �$��
Mustermann��5�   %   �� 5�   "   $��18(�5�   %   $��Light"�5�   %   
���   �$��Nachname��5�   %   5�      �� 5�      $��0,20,0,0q�6)   $ Vertical=�6;   .+�5�   "   �� -   5        $ Username��5�   %   $��24(�5�   %   
���   �$��Mustermann_M��5�   %   �� 5�   "   $��18(�5�   %   $��Light"�5�   %   
���   �$��Benutzername��5�   %   5�      �� 5�      $��0,20,0,0q�6)   $ Vertical=�6;   .+�5�   "   �� -   5        $
 mail��5�   %   $��24(�5�   %   
���   �$"��max.mustermann@mustermail.de��5�   %   �� 5�   "   $��18(�5�   %   $��Light"�5�   %   
���   �$��E-Mail��5�   %   5�      5�      �� 5�      $��1��5�      $��30q�5�      $ Vertical=�5�      .+�5�      '  !MaterialDesignThemes.Wpf.PackIcon  $	��200��5�   !   $	��200��5�   !   $��0,50,0,0q�5�   !   $��Center=�5�   !   $��Center=�5�   !   ���#B9B9B9   Kind$ AccountCircle=�5�   !   G� 5�      -   5         G�Name$ oldPW��5�   !   $��	0,10,0,10q�5�   !   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Bisheriges Kennwort��5�   !    ) %MaterialDesignFilledRevealPasswordBox#��[ 5�   !   {�5�   !   ��  �� ElementName$! RevealPasswordFilledEnabled�� �� Path$ 	IsChecked�5�   !   G� 5�      -   5        $ newPW��5�   !   $��	0,10,0,10q�5�   !   $ Neues Kennwort��5�   !   #��[ 5�   !   {�5�   !   �� $! RevealPasswordFilledEnabled��$ 	IsChecked�5�   !   G� 5�      -	   5        $ newPW2��5�   !   $��	0,10,0,10q�5�   !   $  Neues Kennwort wiederholen��5�   !   #��[ 5�   !   {�5�   !   �� $! RevealPasswordFilledEnabled��$ 	IsChecked�5�   !   �� 5�      -
   5        $ SaveNewPassword��5�   !   $��1��5�   !   $	��200��5�   !   $��0,10,0,0q�5�   !   $��Center=�5�   !   $��Center=�5�   !   $��Neues Kennwort speichern��5�   !     NormalButton#��[ 5�   !   5�      5�      5�      5�      5�      5�      5�      