using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace M17E_TrabalhoModelo_2022_2023.Models
{
    public class Estadia
    {
        [Key]
        public int EstadiaId { get; set; }
        
        [Display(Name ="Data de entrada")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Tem de indicar a data de entrada")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime data_entrada { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime data_saida { get; set;}

        [DataType(DataType.Currency)]
        public decimal valor_pago { get; set;}

        ///////////////////////////////Chave estrangeira
        [ForeignKey("cliente")]
        [Display(Name ="Cliente")]
        [Required(ErrorMessage ="Tem de indicar o cliente")]
        public int ClienteID { get; set; }
        public Cliente cliente { get; set; }

        ///////////////////////////////Chave estrangeira
        [ForeignKey("quarto")]
        [Display(Name ="Quarto")]
        [Required(ErrorMessage ="Tem de indicar um quarto")]
        public int QuartoID { get; set; }

        public Quarto quarto { get; set; }

        //Valor defaul para data de entrada
        public Estadia()
        {
            data_entrada= DateTime.Now;
        }
    }
}