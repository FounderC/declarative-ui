//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab5v18
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publishers
    {
        public Publishers()
        {
            this.Books = new HashSet<Books>();
        }
    
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
    
        public virtual ICollection<Books> Books { get; set; }
    }
}
