using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Validators
{
    /// <summary>
    /// Model used to get the Fluent validation results.
    /// </summary>
    public class ValidationModel
    {
        public ValidationModel()
        {
            IsValid = true;
            ValidationMessages = new List<string>();
        }
        
        /// <summary>
        /// Get's the Model's valid status
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Maintains validation error list
        /// </summary>
        public List<string> ValidationMessages { get; set; }
    }
}
