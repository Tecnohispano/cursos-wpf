   M S B A M L   `   `   `  ����E  ACursos WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Cursos_WPF
Cursos_WPF  5   	       Cursos_WPF.Usuarios   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Cursos_WPF     5   	     � Title$  Usuarios - Cursos WPF��5   	   $	��768��6'   $
��1366��64    � WindowStartupLocation$ CenterScreen=�6A    � WindowState$ Normal=�6f   .��5	      �� $��Stretch=�6   $��Stretch=�6/   .+�5   
   � ���	#FFe0e1fc$	��100��6&   $	��0,0q�63    � ColumnDefinitions 5      �� 5      $��2* �6#   60   �� 5      $��2* �6#   60   �� 5      $��2* �6#   60   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0��62   .+�5      �� -   5        $��18(�5      $
��Bold"�6'   $��Center=�69   $
��10,0q�6T   ���	#FFe0e1fc���	#FFe0e1fc.��6�   INICIO6�   �� 5      -   5        $��18(�5      $
��Bold"�6'   $��Center=�69   $
��10,0q�6T   ���	#FFe0e1fc���	#FFe0e1fc.��6�   CURSOS6�   �� 5      $��18(�6   $
��Bold"�6'   $��Center=�69   $
��10,0q�6T   ���	#FFe0e1fc���	#FFe0e1fc.��6�   
USUARIOS6�   5      �� 5      $��Images/cursos-wpf.png��6   $��1��63   $��10, 5, 10, 20q�6C   �� 5      $��
Cursos WPF��6   $��1��6)   $��Center=�69   $��Bottom=�6V   $
��Bold"�6q   �� 5      $!��Bienvenido, @freddy.iniguez��6   $��2��6:   $��14(�6J   $
��Bold"�6X   $��Center=�6j   $��Right=�6�   $��50, 0q�6�   �� 5      $�� ��6   $��2��6    $��30��60   $��30��6<   $��Center=�6G   $��Right=�6b   $��10, 0q�6~   ��5      �� 5        �� ImageSource$ Images/logout.png��6!   5"      5$      �� 5%   
   $��50��6   $
��20,0q�6    � 5(   
   $	��550��6   $
��10,0q�6   .+�5)      �� .+�5*      �� $��Gestión de Usuarios��6   $��30(�66   $
��Bold"�6D   6X   � 5,       5-      �� 5.      $
��1.5* �6+   6:   �� 5/      $
��1.5* �6+   6:   �� 50      $
��1.5* �6+   6:   �� 51      $��1* �6+   68   �� 52      $��1* �6+   68   .+�56      �� $��0��6!   $��10q�61   .+�57      �� $��Primer nombre��6    6:   �� 58      -   5         ��Name$ PrimerNombre��58   "   6:   �� 59      $��Segundo nombre��6    $��0, 15, 0, 0q�69   6P   �� 5:      -   5        $ SegundoNombre��5:   "   6;   �� 5;      $��Apellido paterno��6    $��0, 15, 0, 0q�6;   6R   �� 5<      -   5        $ ApellidoPaterno��5<   "   6=   �� 5=      $��Apellido materno��6    $��0, 15, 0, 0q�6;   6R   �� 5>      -   5        $ ApellidoMaterno��5>   "   6=   5?      �� 5B      $��1��6!   $��10q�61   .+�5C      �� $��Correo electrónico��6    6?   �� 5D      -   5        $ Correo��5D   "   65   5E      �� 5H      $��2��6!   $��10q�61   .+�5I      �� $��Tipo de usuario��6    6<   �� 5J      -   5         ��Name$ CmbTipoUsuario��5J   #   5K      �� 5N      $��4��6!   $��10q�61   .+�5O      �� -	   5         ��Name$ 
btnAgregar��5O   !   $��20��65   $��80��6A   $	��Top=�6L   $��0, 20q�6d   
��� � �
�������.��6�   	Agregar6�   5P      5Q      5R      5S      �� 5V   
   $7��1Cursos WPF, 2020 © Todos los derechos reservados��6   $��Center=�6K   $��Center=�6f   6�   5W      5X      