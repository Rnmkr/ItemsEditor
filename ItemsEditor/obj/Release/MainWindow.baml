   M S B A M L   `   `   `  ���� F  BItemsEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ItemsEditorItemsEditor      ItemsEditor.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:ItemsEditor       � Title$"  Editor de Items [Produccion]��$	��575��$
��1305�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$ Resources/itemsicon.png��.��� $
��Left=�$	��Top=�$��0,0,0,0q�$
��1290��.+��� -   $��ComboProducto�� �� 
IsEditable .  �� Text$ SELECCIONAR��$
��Left=�$��	10,34,0,0q�$	��Top=�$	��300��$��24��$��Center=�$��Center=�$
��Bold"�$��Consolas)�$��14(��� -   $��ComboModelo�� . $ SELECCIONAR��$
��Left=�$��	10,95,0,0q�$	��Top=�$	��300��$��24��$��Center=�$��Center=�$
��Bold"�$��Consolas)�$��14(��� -   $��ComboArticulo�� . $ SELECCIONAR��$
��Left=�$��
10,156,0,0q�$	��Top=�$	��300��$��24��$��Center=�$��Center=�$
��Bold"�$��Consolas)�$��14(��� -   $��ComboCategoria�� . $ SELECCIONAR��$
��Left=�$��
10,217,0,0q�$	��Top=�$	��300��$��24��$��Center=�$��Center=�$
��Bold"�$��Consolas)�$��14(��� -   $��ComboVersion�� . $ SELECCIONAR��$
��Left=�$��
10,278,0,0q�$	��Top=�$	��300��$��24��$��Center=�$
��Bold"�$��Consolas)�$��14(�$��Center=��� -   $��TextBoxDescripcion��$
��Left=�$��24��$��
10,339,0,0q� �� TextWrapping$ NoWrap=�$��INGRESAR DESCRIPCION��$	��Top=�$	��300��$��Center=�$
��Bold"�$��Consolas)�$��14(� �� MaxLines$ 1��$��Center=�.������  �� Color# � ��.���� -   $��TextBoxUUID��$
��Left=�$��24��$��
10,397,0,0q�$ NoWrap=�$��INGRESAR UUID��$	��Top=�$	��300��$��Center=�$��Center=�$
��Bold"�$��Consolas)�$��14(�$ 1��.������ # � ��.���� -   $��Borrar��$��BORRAR��$
��Left=�$	��Top=�$	��300��$��
10,466,0,0q�$��24��$��Consolas)�$
��Bold"�$��14(�	 � ToolTip$4	 .Borra la entrada del Item de la Base de Datos.���� -	   $��Reset��$��RESETEAR CAMPOS��$
��Left=�$	��Top=�$	��300��$��
10,437,0,0q�$��24��$��Consolas)�$
��Bold"�$��14(�$E	 ?Vuelve todos los campos de este formulario a su estado inicial.���� -
   $��Agregar��$��AGREGAR��$
��Left=�$	��Top=�$	��300��$��
10,495,0,0q�$��24��$��Consolas)�$
��Bold"�$��14(�$(	 "Agrega el Item a la Base de Datos.���� $	��960��$	��480��$��Right=�$	��Top=�$��	0,10,10,0q�$��1q�
���   �._��� -   $��ImageDisplay��$2��,/ItemsEditor;component/Resources/nophoto.png��$��Stretch=�$	��480��$��0,0,0,0q�$��Stretch=�$	��960���� -   $��TextBoxFile��$
��Left=�$��32��$��320,495,0,0q�$ NoWrap=�$(��"SELECCIONAR ARCHIVO .JPG (960X480)��$	��Top=�$	��923��$��Consolas)�$
��Bold"�$��14(�$��Center=�$ 1��$/	 )Muestra la ruta del archivo seleccionado.���� -   $��ButtonArchivo��$	��...��$
��Left=�$��1248,495,0,0q�$	��Top=�$��32��$��32��$��Consolas)�$
��Bold"�$��14(�$_	 YHaga clic para SELECCIONAR el archivo de imagen que muestra el Item que esta por agregar.���� $��PRODUCTO��$
��Left=�$��	10,12,0,0q�$	��Top=�$	��300��$��Consolas)�$
��Bold"�$��Center=�$��10(�$��Right=��� $��MODELO��$
��Left=�$��	10,73,0,0q�$	��Top=�$	��300��$��Consolas)�$
��Bold"�$��10(�$��Center=�$��Right=��� $��ARTICULO��$
��Left=�$��
10,134,0,0q�$	��Top=�$	��300��$��Consolas)�$
��Bold"�$��10(�$��Center=�$��Right=��� $��VERSION��$
��Left=�$��
10,258,0,0q�$	��Top=�$	��300��$��Consolas)�$
��Bold"�$��10(�$��Center=�$��Right=��� $��	CATEGORIA��$
��Left=�$��
10,195,0,0q�$	��Top=�$	��300��$��Consolas)�$
��Bold"�$��10(�$��Center=�$��Right=��� $��DESCRIPCION��$
��Left=�$��
10,317,0,0q�$	��Top=�$	��300��$��Consolas)�$
��Bold"�$��10(�$��Center=�$��Right=��� $
��UUID��$
��Left=�$��
10,375,0,0q�$	��Top=�$	��300��$��Consolas)�$
��Bold"�$��10(�$��Center=�$��Right=�