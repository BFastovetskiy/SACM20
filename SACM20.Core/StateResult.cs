using System;
using System.Collections.Generic;
using System.Text;

namespace SACM20.Core
{
    public class StateResult
    {
        private Guid m_id = Guid.Empty;

        /// <summary>
        /// Result operation in bool expression
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Code operation
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Type of object
        /// </summary>
        public string ObjectType { get; set; }

        /// <summary>
        /// Method returning result
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Identifier object
        /// </summary>
        public Guid ObjectId
        {
            get { return this.m_id; }
            set { this.m_id = value; }
        }

        /// <summary>
        /// Constructor of instance
        /// </summary>
        public StateResult() : base()
        { }

        /// <summary>
        /// Advanced constructor of instance
        /// </summary>
        public StateResult(IBaseClass baseClass) : base()
        {
            this.ObjectId = baseClass.Id;
            this.ObjectType = baseClass.GetType().Name;
        }

        public override string ToString()
        {
            return string.Format("Result: {1}{0}Code: {2}{0}Message: {3}{0}Object: {4}{0}Method: {5}",
                Environment.NewLine, Result, Code, Message, this.ObjectType, Method);
        }

        public static StateResult Success
        {
            get { return new StateResult() { Result = true, Code = 0, Message = "Success" }; }
        }

        public static StateResult NotUsedModuleChangeProcessing
        {
            get { return new StateResult() { Result = false, Code = -1, Message = "Not used module change processing" }; }
        }

        public static StateResult ObjectIsNotUnique
        {
            get { return new StateResult() { Result = false, Code = -2, Message = "Object is not unique" }; }
        }

        public static StateResult ObjectIsUsed
        {
            get { return new StateResult() { Result = false, Code = -3, Message = "The object is used" }; }
        }

        public static StateResult ThereAreNoResources
        {
            get { return new StateResult() { Result = false, Code = -4, Message = "There are no resources" }; }
        }

        public static StateResult NotFound
        {
            get { return new StateResult() { Result = false, Code = -5, Message = "Object not found" }; }
        }

        public static StateResult Forbidden
        {
            get { return new StateResult() { Result = false, Code = -6, Message = "Implementation of the action forbidden" }; }
        }

        public static StateResult NotConnected
        {
            get { return new StateResult() { Result = false, Code = -7, Message = "Unable to establish a database connection" }; }
        }
    }
}
