   M S B A M L   `   `   `  ����F  BItemsEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ItemsEditorItemsEditor  5   	       ItemsEditor.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ItemsEditor     5   	     � Title$"  Editor de Items [Produccion]��5   	   $	��575��6.   $
��1305��6;    � 
ResizeMode$ CanMinimize=�6H    � WindowStartupLocation$ CenterScreen=�6a    � Icon$ Resources/itemsicon.png��6�   .��5	      � $
��Left=�6   $	��Top=�6&   $��0,0,0,0q�6>   $
��1290��6O   .+�5
   
   �� -   5        $��ComboProducto��5
       �� 
IsEditable .  �� Text$ SELECCIONAR��6�   $
��Left=�6;  $��	10,34,0,0q�6V  $	��Top=�6i  $	��300��6�  $��24��6�  $��Center=�6�  $��Center=�6�  $
��Bold"�6�  $��Consolas)�6�  $��14(�6  �� 5   
   -   5        $��ComboModelo��5       . $ SELECCIONAR��6�   $
��Left=�6/  $��	10,95,0,0q�6J  $	��Top=�6]  $	��300��6u  $��24��6�  $��Center=�6�  $��Center=�6�  $
��Bold"�6�  $��Consolas)�6�  $��14(�6�  �� 5   
   -   5        $��ComboArticulo��5       . $ SELECCIONAR��6�   $
��Left=�6;  $��
10,156,0,0q�6V  $	��Top=�6j  $	��300��6�  $��24��6�  $��Center=�6�  $��Center=�6�  $
��Bold"�6�  $��Consolas)�6�  $��14(�6  �� 5   
   -   5        $��ComboCategoria��5       . $ SELECCIONAR��6�   $
��Left=�6A  $��
10,217,0,0q�6\  $	��Top=�6p  $	��300��6�  $��24��6�  $��Center=�6�  $��Center=�6�  $
��Bold"�6�  $��Consolas)�6�  $��14(�6  �� 5   
   -   5        $��ComboVersion��5       . $ SELECCIONAR��6�   $
��Left=�65  $��
10,278,0,0q�6P  $	��Top=�6d  $	��300��6|  $��24��6�  $��Center=�6�  $
��Bold"�6�  $��Consolas)�6�  $��14(�6�  $��Center=�6�  �� 5   
   -   5        $��TextBoxDescripcion��5      $
��Left=�6�   $��24��6�   $��
10,339,0,0q�6�    �� TextWrapping$ NoWrap=�6  $��INGRESAR DESCRIPCION��6  $	��Top=�66  $	��300��6N  $��Center=�6Z  $
��Bold"�6|  $��Consolas)�6�  $��14(�6�   �� MaxLines$ 1��6�  $��Center=�6�  .��6�  ��5      �� 5       �� Color# � ��6"   .��5   "   5      �� 5   
   -   5        $��TextBoxUUID��5      $
��Left=�6�   $��24��6�   $��
10,397,0,0q�6�   $ NoWrap=�6�   $��INGRESAR UUID��6�   $	��Top=�6�   $	��300��6  $��Center=�6"  $��Center=�6F  $
��Bold"�6h  $��Consolas)�6z  $��14(�6�  $ 1��6�  .��6�  ��5      �� 5      # � ��6"   .��5   "   5      �� 5   
   -   5        $��Borrar��5      $��BORRAR��6   $
��Left=�60   $	��Top=�6K   $	��153��6c   $��
10,466,0,0q�6o   $��24��6�   $��Consolas)�6�   $
��Bold"�6�   $��14(�6�   	 � ToolTip$4	 .Borra la entrada del Item de la Base de Datos.��6�   6�   �� 5   
   -	   5        $��Reset��5      $��RESETEAR CAMPOS��6   $
��Left=�68   $	��Top=�6S   $	��153��6k   $��
10,437,0,0q�6w   $��24��6�   $��Consolas)�6�   $
��Bold"�6�   $��14(�6�   $E	 ?Vuelve todos los campos de este formulario a su estado inicial.��6�   6  �� 5   
   -
   5        $��Agregar��5      $��AGREGAR��6    $
��Left=�62   $	��Top=�6M   $	��142��6e   $��168,466,0,0q�6q   $��24��6�   $��Consolas)�6�   $
��Bold"�6�   $��14(�6�   $(	 "Agrega el Item a la Base de Datos.��6�   6�   �� 5!   
   $	��960��6   $	��480��6   $��Right=�6*   $	��Top=�6F   $��	0,10,10,0q�6^   $��1q�6q   
���   �._�5"      �� -   5        $��ImageDisplay��5"      $2��,/ItemsEditor;component/Resources/nophoto.png��6(   $��Stretch=�6^   $	��480��6|   $��0,0,0,0q�6�   $��Stretch=�6�   $	��960��6�   5#      �� 5$   
   -   5        $��TextBoxFile��5$      $
��Left=�6%   $��32��6@   $��320,495,0,0q�6L   $ NoWrap=�6a   $(��"SELECCIONAR ARCHIVO .JPG (960X480)��6w   $	��Top=�6�   $	��923��6�   $��Consolas)�6�   $
��Bold"�6�   $��14(�6�   $��Center=�6�   $ 1��6  $/	 )Muestra la ruta del archivo seleccionado.��6*  �� 5%   
   -   5        $��ButtonArchivo��5%      $	��...��6&   $
��Left=�6G   $��1248,495,0,0q�6b   $	��Top=�6x   $��32��6�   $��32��6�   $��Consolas)�6�   $
��Bold"�6�   $��14(�6�   $_	 YHaga clic para SELECCIONAR el archivo de imagen que muestra el Item que esta por agregar.��6�   �� 5&   
   $��PRODUCTO��6   $
��Left=�6#   $��	10,12,0,0q�6>   $	��Top=�6Q   $	��300��6i   $��Consolas)�6u   $
��Bold"�6�   $��Center=�6�   $��10(�6�   $��Right=�6�   �� 5'   
   $��MODELO��6   $
��Left=�6!   $��	10,73,0,0q�6<   $	��Top=�6O   $	��300��6g   $��Consolas)�6s   $
��Bold"�6�   $��10(�6�   $��Center=�6�   $��Right=�6�   �� 5(   
   $��ARTICULO��6   $
��Left=�6#   $��
10,134,0,0q�6>   $	��Top=�6R   $	��300��6j   $��Consolas)�6v   $
��Bold"�6�   $��10(�6�   $��Center=�6�   $��Right=�6�   �� 5)   
   $��VERSION��6   $
��Left=�6"   $��
10,258,0,0q�6=   $	��Top=�6Q   $	��300��6i   $��Consolas)�6u   $
��Bold"�6�   $��10(�6�   $��Center=�6�   $��Right=�6�   �� 5*   
   $��	CATEGORIA��6   $
��Left=�6$   $��
10,195,0,0q�6?   $	��Top=�6S   $	��300��6k   $��Consolas)�6w   $
��Bold"�6�   $��10(�6�   $��Center=�6�   $��Right=�6�   �� 5+   
   $��DESCRIPCION��6   $
��Left=�6&   $��
10,317,0,0q�6A   $	��Top=�6U   $	��300��6m   $��Consolas)�6y   $
��Bold"�6�   $��10(�6�   $��Center=�6�   $��Right=�6�   �� 5,   
   $
��UUID��6   $
��Left=�6   $��
10,375,0,0q�6:   $	��Top=�6N   $	��300��6f   $��Consolas)�6r   $
��Bold"�6�   $��10(�6�   $��Center=�6�   $��Right=�6�   �� 5-   
   -   5        
 ��Name$
 ButtonListar��5-      $��	VER LISTA��6'   $
��Left=�6;   $	��Top=�6V   $	��142��6n   $��168,437,0,0q�6z   $��24��6�   $��Consolas)�6�   $
��Bold"�6�   $��14(�6�   $E	 ?Vuelve todos los campos de este formulario a su estado inicial.��6�   6  5.      5/      