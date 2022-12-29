namespace Dominio


{
    public  class IndexViewModel
    {
        public bool Visible { get; set; }
        public Documento Documento { get; set; }
        public Proveedor Proveedor { get; set; }
        public DatosProveedor DatosProveedor { get; set; }
        public List<Documento> DocumentoList { get; set; }
        public List<Proveedor> ProveedorList { get; set; }   
        public List<Usuario> UsuarioList { get; set; }
        public string Area { get; set; }
 
    }
}
