   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null�Pclr-namespace:waerp_management.application.Administration.LocationAdministrationBwaerp_management.application.Administration.LocationAdministration  5      ]    Wwaerp_management.application.Administration.LocationAdministration.LocationOverviewView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      blocalPclr-namespace:waerp_management.application.Administration.LocationAdministration     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      ���#EEEEEE   MaterialDesignFont#���   5      
���   �$��13(�5      $��Regular"�5        � 	Resources  5        QuantityCell �� 
TargetType �� Property �� Value  SingleColDataGrid  GridCellPad  LeftGridHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  CenterGridHeaderStyle��%A  &	       &	 �     &	 !    &	 �    &	 �    �� 5   
   " ��6%   .�5      ��  �@��TextAlignment$ Center=�6.   �� 5       � ��$ 0,0,0,0q�6'   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6*   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 15,0,0,0q�6'   5      �� 5   
   " ��6$   .�5      ��  � ��$ 15,0,0,0q�6'   �� 5       � ��$
 Wrap=�6-   �� 5       � ��$ Center=�62   5      �� 5    
   "  6,   .�5!      ��  � ��$
 Left=�6;   �� 5"       � ��
 ���� 6+   �� 5#       � �� �#4444446+   �� 5$       � ��$ 15,0,0,0q�6'   �� 5%       � ��$ Medium"�6+   �� 5&       � ��$ 13(�6)   �� 5'       �@�Cursor$
 Hand��6'   5(      �� 5)   
   "  6.   .�5*      ��  � ��$ Center=�6;   �� 5+       � ��
 ���� 6+   �� 5,       � �� �#4444446+   �� 5-       � ��$ Medium"�6+   �� 5.       � ��$ 13(�6)   �� 5/       �@�Cursor$
 Hand��6'   50      51      .��52      �   GridBackgroundMainFrame#��[ 6    � RowDefinitions 53   
   �� 54      $��* �6   �� 55      $��5* �6   .+�57   
   � $��0��58      $��	50,0,50,0q�59      $��Center=�5:       � ColumnDefinitions 5;      �� 5<      $��3* �6#   �� 5=      $��* �6#   .+�5?      �� $��0��5@      $��Stretch=�5A      $��16(�5B      $��Light"�5C       �� TextAlignment$
 Left=�5D      $
��Wrap=�5E      53Hier können Sie Lagerorte bearbeiten oder anlegen.�� 5F   D   �� 6Q   �~Sollten Sie einen Lagerorte bearbeiten müssen, dann klicken Sie auf den Stift in der Tabelle beim zu bearbeitenden Lagerorte.5G   �   �� 5H      $��1��6    �� Orientation$ Vertical=�6)   .+�5I      �� -   5        $	��170��5J      $��0,0,0,20q�5K      $��Right=�5L      $��Lagerort anlegen��5N        NormalButton#��[ 5O       � ToolTip$ Lagerort anlegen��5P      �� 5Q      -   5        	 ��Name$	 ExportAsCSV��5R      $	��240��5S      $��Right=�5T      $!��Lagerorte exportieren (CSV)��5V      #��[ 5W      $ Lagerort anlegen��5X      5Y      5\      � 5]   
   $��1��6    5^      �� 5_      $��* �6    �� 5`      $��7* �6     5b      �� 5c      $��* �6#   �� 5d      $��* �6#   .+�5f      �� -   5        
 ��Name$
 	searchBox��5g      $��0��5h      $��	50,0,20,0q�5i      $��Stretch=�5j      $	��Top=�5k      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Suche��5l       �� AcceptsReturn .  � Cursor$
 Hand��5n      {�.   MaterialDesignFilledTextBox#��[ 5p       �� TextWrapping$ NoWrap=�5r      &   System.Windows.Controls.DataGrid  5s      -   5          Name$ DataLocationItems��5t      $��1��5u      $��
50,0,20,20q�5v      ��5w      ��  �� Path$ itemData�   	CellStyle 5z      �� 5{      *  $System.Windows.Controls.DataGridCell"  6   .�5|      ��  � ��$ 0q�6<   �� 5}       �@�FocusVisualStyle 6=   Z� �� 5~       � ��$ 14(�65   �� 5       � ��$	 100��66   �� 5�       � ��$	 100��66    �� Triggers 5�      P� 5�       P� Property �@ 
IsSelected P� Value . . �5�   "   ��  � �� �#A94C426?   �� 5�   "    � ��
 �����6?   5�      5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5�      1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5�         Header$ Lagerort��5�         HeaderStyle# [ 5�         
IsReadOnly .    Binding 5�      �� $ location_name�5�        5�      $ 2* 5�      # [ 5�      $ Gelagerte Menge��5�      # [ 5�       .  5�      �� $ location_quantity�5�      4	  .System.Windows.Controls.DataGridTemplateColumn	  5�      $ * 5�      $ 
Bearbeiten��5�      # [ 5�       .  	  CellTemplate 5�      �� 5�      .R�5�   "   �� $	 EditLocation��5�   %   -   5        $��40��5�   %   $��40��5�   %   ���#4265A9
������ $��0q�5�   %   
������� +	 'MaterialDesignFloatingActionLightButton#��[	 5�   %   .��5�   &   '
  !MaterialDesignThemes.Wpf.PackIcon
   
  Kind$ PencilOutline=�6>   5�   #   5�      5�      	  5�      $ * 5�      $ Löschen��5�      # [ 5�       .  5�      �� 5�      .R�5�   "   �� $	 DeleteLocation��5�   %   -   5        $��40��5�   %   $��40��5�   %   ���#A94C42
������ $��0q�5�   %   
�������#��[	 5�   %   .��5�   &   
  $ DeleteOutline=�6>   5�   #   5�      5�      5�        5�      -   5        $ GroupItemData��5�      $��2��5�      $��1��5�      $��
20,0,50,20q�5�       5�      �� 5�      "  6   .�5�      ��  � ��$ 0q�6<   �� 5�       �@�FocusVisualStyle 6=   Z� �� 5�       � ��$ 14(�65   �� 5�       � ��$	 100��66   �� 5�       � ��$	 100��66    5�      P� 5�       �@ 
IsSelected . . �5�   "   ��  � �� �#9E9E9E6?   �� 5�   "    � ��
 �����6?   5�      5�       5�        5�      $ * 5�      # [ 5�      $ Artikelnummer��5�      # [ 5�       .  5�      �� $ 
item_ident�5�        5�      $ 2* 5�      # [ 5�      $ Beschreibung��5�      # [ 5�       .  5�      �� $ item_description�5�        5�      $ * 5�      # [ 5�      $ Bestand��5�      # [ 5�       .  5�      �� $ location_quantity�5�      	  5      $ 2* 5     $
 Bild��5     # [ 5      .  5     �� 5     .R�5  "   �� $	��100��5  %    �� BitmapScalingMode$ HighQuality=�5	  %   ��5
  %   �� $ item_image_path�5     5     5     5     5     5     