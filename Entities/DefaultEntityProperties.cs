using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AspCoreVue.Entities
{
    public class DefaultEntityProperties
    {
        public DefaultEntityProperties()
        {
            SavedUserId = 0;
            SavedDate = DateTime.Now;
            RemovedDate = null;
        }
        [Required]
        public virtual int SavedUserId { get; set; }
        [Required]
        public virtual DateTime SavedDate { get; set; }
        public virtual DateTime? RemovedDate { get; set; }
    }
}