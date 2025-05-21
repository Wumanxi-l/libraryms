using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Borrow
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        /// <summary>
        /// 借书时间
        /// </summary>
        public DateTime BorrowTime { get; set; }
        /// <summary>
        /// 应还时间
        /// </summary>
        public DateTime DueTime { get; set; } 
        /// <summary>
        /// 还书时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }
        public bool IsReturn { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}
