   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=nullq=clr-namespace:waerp_management.application.ItemAdministration/waerp_management.application.ItemAdministration  5      F    @waerp_management.application.ItemAdministration.ItemOverviewView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Olocal=clr-namespace:waerp_management.application.ItemAdministration     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      ���#EEEEEE   MaterialDesignFont#���   5      
���   �$��13(�5      $��Regular"�5        � 	Resources  5        QuantityCell �� 
TargetType �� Property �� Value  SingleColDataGrid  GridCellPad  LeftGridHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  CenterGridHeaderStyle��%�  &	       &	 �     &	 �     &	 o    &	 �    �� 5   
   " ��6%   .�5      ��  �@��TextAlignment$ Center=�6.   �� 5       � ��$ 0,10,0,0q�6'   5      �� 5   
   " ��6*   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 15,0,0,0q�6'   5      �� 5   
   " ��6$   .�5      ��  � ��$ 	15,10,0,0q�6'   �� 5       � ��$
 Wrap=�6-   5      �� 5   
   "  6,   .�5      ��  � ��$
 Left=�6;   �� 5        � ��
 ���� 6+   �� 5!       � �� �#4444446+   �� 5"       � ��$ 15,0,0,0q�6'   �� 5#       � ��$ Medium"�6+   �� 5$       � ��$ 13(�6)   �� 5%       �@�Cursor$
 Hand��6'   5&      �� 5'   
   "  6.   .�5(      ��  � ��$ Center=�6;   �� 5)       � ��
 ���� 6+   �� 5*       � �� �#4444446+   �� 5+       � ��$ Medium"�6+   �� 5,       � ��$ 13(�6)   �� 5-       �@�Cursor$
 Hand��6'   5.      5/      .��52      �   GridBackgroundMainFrame#��[ 6    � ColumnDefinitions 53   
   �� 54      $��* �6   �� 55      $��* �6    � RowDefinitions 57   
   �� 58      $��* �6   �� 59      $
��0.5* �6   �� 5:      $��6* �6   .+�5<   
   �� $��0��5=      $��	50,20,0,0q�5>      $��Stretch=�5?      $��16(�5@      $��Light"�5A       �� TextAlignment$
 Left=�5B      $
��Wrap=�5C      ��Hier können Sie Artikel bearbeiten oder anlegen. Bitte beachten Sie beim Anlegen darauf den richtigen Kunden für den Artikel zu wählen.�� 5D   �   �� 6�   zxSollten Sie ein Artikel bearbeiten müssen, dann klicken Sie auf den Stift in der Tabelle beim zu bearbeitenden Artikel.5E   �   �� 5I   
   $��0��5J      $��1��5K      $��	0,20,50,0q�5L      $��Right=�5M       �� Orientation$ 
Horizontal=�5N      .+�5O      �� -   5        $��1��5P      $	��170��5Q      $��0,0,50,0q�5R      $��Right=�5S      $	��Top=�5T      $��Artikel anlegen��5V        NormalButton#��[ 5W       � ToolTip$ Lagerort anlegen��5X      �� 5Y      $��1��5Z      $	��Top=�5[      $ Vertical=�5\      .+�5^      �� -   5        	 ��Name$	 AddItemFilter��5_      $��1��5`      $	��170��5a      $��0,0,0,20q�5b      $��Right=�5c      $��Filter anlegen��5e      #��[ 5f      $ Lagerort anlegen��5g      �� 5h      -   5        $	 EditItemFilter��5i      $��1��5j      $	��170��5k      $��Right=�5l      $��Filter bearbeiten��5n      #��[ 5o      $ Lagerort anlegen��5p      5r      5s      �� 5t   
   -   5        
 ��Name$
 	searchBox��5u      $��1��5v      $��2��5w      $��	50,0,50,0q�5x      $��Stretch=�5y      $��Center=�5z      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Suche��5{       �� AcceptsReturn .   � Cursor$
 Hand��5}      {�.   MaterialDesignFilledTextBox#��[ 5       �� TextWrapping$ NoWrap=�5�      &   System.Windows.Controls.DataGrid  5�   
   -   5          Name$ dataGridItems��5�      $��2��5�      $��2��5�      $��50,20,50,20q�5�      ��5�      ��  �� Path$ itemData� � ContextMenu 5�      �� 5�      .@�5�      m� -   5        $��Artikelnummer kopieren��5�   5   m� 5�      -   5        $��Beschreibung kopieren��5�   7   m� 5�      -   5        $��Zeile kopieren��5�   /   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6   .�5�      ��  � ��$ 0q�68   �� 5�       �@�FocusVisualStyle 69   Z� �� 5�       � ��$ 14(�61   �� 5�       � ��$	 100��62   �� 5�       � ��$	 100��62    �� Triggers 5�      P� 5�       P� Property �@ 
IsSelected P� Value . . �5�      ��  � �� �#A94C426;   �� 5�       � ��
 �����6;   5�      5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5�      1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5�         Header$ Artikelnummer��5�         HeaderStyle# [ 5�         
IsReadOnly .    Binding 5�      �� $ 
item_ident�5�        5�      $ 2* 5�      # [ 5�      $ Beschreibung��5�      # [ 5�       .  5�      �� $ item_description�5�        5�      $ * 5�      # [ 5�      $ Bestand��5�      # [ 5�       .  5�      �� $ item_quantity_total�5�      4	  .System.Windows.Controls.DataGridTemplateColumn	  5�      $ 2* 5�      $
 Bild��5�      # [ 5�       .  	  CellTemplate 5�      �� 5�      .R�5�      �� $	��100��5�   !    �� BitmapScalingMode$ HighQuality=�5�   !   ��5�   !   �� $ item_image_path�5�      5�      	  5�      $ * 5�      $ 
Bearbeiten��5�      # [ 5�       .  5�      �� 5�      .R�5�      �� $	 EditItem��5�   !   -	   5        $��40��5�   !   $��40��5�   !   ���#4265A9
������ $��0q�5�   !   
������� +	 'MaterialDesignFloatingActionLightButton#��[	 5�   !   .��5�   "   '
  !MaterialDesignThemes.Wpf.PackIcon
   
  Kind$ PencilOutline=�6:   5�      5�      5�      	  5�      $ * 5�      $ Löschen��5�      # [ 5�       .  5�      �� 5�      .R�5�      �� $	 
DeleteItem��5�   !   -
   5        $��40��5�   !   $��40��5�   !   ���#A94C42
������ $��0q�5�   !   
�������#��[	 5�   !   .��5�   "   
  $ DeleteOutline=�6:   5�      5�      5�      5�      5�      5�      