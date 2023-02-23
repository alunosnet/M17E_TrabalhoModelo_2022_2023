using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M17E_TrabalhoModelo_2022_2023.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }

        [Required(ErrorMessage ="Tem de indicar o nome do cliente")]
        [StringLength(80)]
        [MinLength(3,ErrorMessage ="Nome muito pequeno. Tem de ter pelo menos 3 letras")]
        [UIHint("Insira o nome do cliente, deve ter pelo menos 3 letras")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Tem de indicar a morada do cliente")]
        [StringLength(80)]
        [MinLength(3, ErrorMessage = "Morada muito pequena. Tem de ter pelo menos 3 letras")]
        [UIHint("Insira a morada do cliente, deve ter pelo menos 3 letras")]
        public string Morada { get; set; }

        [Required(ErrorMessage = "Tem de indicar o código postal do cliente")]
        [StringLength(8)]
        [MinLength(8, ErrorMessage = "O código postal tem de ter 8 letras")]
        [UIHint("Insira o código postal do cliente")]
        [Display(Name ="Código Postal")]
        public string CP { get; set; }
        
        [Required(ErrorMessage ="Tem de indicar um email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Tem de indicar um telefone")]
        [StringLength(15)]
        [MinLength(9, ErrorMessage = "O telefone deve ter pelo menos 9 digitos")]
        public string Telefone { get; set; }

        [Required(ErrorMessage ="Tem de indicar a data de nascimento do cliente")]
        [Display(Name ="Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime DataNascimento { get; set; }

        //lista das estadias
        public virtual List<Estadia> listaEstadias { get; set; }
    }
}