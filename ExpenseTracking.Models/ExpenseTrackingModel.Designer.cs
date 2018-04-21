﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("FooModel", "FK_Expense_tbl_Tag_tbl", "Tag_tbl", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(ExpenseTracking.Models.Tag_tbl), "Expense_tbl", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ExpenseTracking.Models.Expense_tbl), true)]

#endregion

namespace ExpenseTracking.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class FooEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new FooEntities object using the connection string found in the 'FooEntities' section of the application configuration file.
        /// </summary>
        public FooEntities() : base("name=FooEntities", "FooEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new FooEntities object.
        /// </summary>
        public FooEntities(string connectionString) : base(connectionString, "FooEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new FooEntities object.
        /// </summary>
        public FooEntities(EntityConnection connection) : base(connection, "FooEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Tag_tbl> Tag_tbl
        {
            get
            {
                if ((_Tag_tbl == null))
                {
                    _Tag_tbl = base.CreateObjectSet<Tag_tbl>("Tag_tbl");
                }
                return _Tag_tbl;
            }
        }
        private ObjectSet<Tag_tbl> _Tag_tbl;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Expense_tbl> Expense_tbl
        {
            get
            {
                if ((_Expense_tbl == null))
                {
                    _Expense_tbl = base.CreateObjectSet<Expense_tbl>("Expense_tbl");
                }
                return _Expense_tbl;
            }
        }
        private ObjectSet<Expense_tbl> _Expense_tbl;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Tag_tbl EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTag_tbl(Tag_tbl tag_tbl)
        {
            base.AddObject("Tag_tbl", tag_tbl);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Expense_tbl EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToExpense_tbl(Expense_tbl expense_tbl)
        {
            base.AddObject("Expense_tbl", expense_tbl);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="FooModel", Name="Expense_tbl")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Expense_tbl : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Expense_tbl object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static Expense_tbl CreateExpense_tbl(global::System.Int32 id)
        {
            Expense_tbl expense_tbl = new Expense_tbl();
            expense_tbl.ID = id;
            return expense_tbl;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> ExpenseDate
        {
            get
            {
                return _ExpenseDate;
            }
            set
            {
                OnExpenseDateChanging(value);
                ReportPropertyChanging("ExpenseDate");
                _ExpenseDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ExpenseDate");
                OnExpenseDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _ExpenseDate;
        partial void OnExpenseDateChanging(Nullable<global::System.DateTime> value);
        partial void OnExpenseDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                OnAmountChanging(value);
                ReportPropertyChanging("Amount");
                _Amount = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Amount");
                OnAmountChanged();
            }
        }
        private Nullable<global::System.Decimal> _Amount;
        partial void OnAmountChanging(Nullable<global::System.Decimal> value);
        partial void OnAmountChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> TagID
        {
            get
            {
                return _TagID;
            }
            set
            {
                OnTagIDChanging(value);
                ReportPropertyChanging("TagID");
                _TagID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("TagID");
                OnTagIDChanged();
            }
        }
        private Nullable<global::System.Int32> _TagID;
        partial void OnTagIDChanging(Nullable<global::System.Int32> value);
        partial void OnTagIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                OnUserIDChanging(value);
                ReportPropertyChanging("UserID");
                _UserID = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("UserID");
                OnUserIDChanged();
            }
        }
        private global::System.String _UserID;
        partial void OnUserIDChanging(global::System.String value);
        partial void OnUserIDChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("FooModel", "FK_Expense_tbl_Tag_tbl", "Tag_tbl")]
        public Tag_tbl Tag_tbl
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Tag_tbl>("FooModel.FK_Expense_tbl_Tag_tbl", "Tag_tbl").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Tag_tbl>("FooModel.FK_Expense_tbl_Tag_tbl", "Tag_tbl").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Tag_tbl> Tag_tblReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Tag_tbl>("FooModel.FK_Expense_tbl_Tag_tbl", "Tag_tbl");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Tag_tbl>("FooModel.FK_Expense_tbl_Tag_tbl", "Tag_tbl", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="FooModel", Name="Tag_tbl")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Tag_tbl : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Tag_tbl object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        public static Tag_tbl CreateTag_tbl(global::System.Int32 id)
        {
            Tag_tbl tag_tbl = new Tag_tbl();
            tag_tbl.ID = id;
            return tag_tbl;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("FooModel", "FK_Expense_tbl_Tag_tbl", "Expense_tbl")]
        public EntityCollection<Expense_tbl> Expense_tbl
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Expense_tbl>("FooModel.FK_Expense_tbl_Tag_tbl", "Expense_tbl");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Expense_tbl>("FooModel.FK_Expense_tbl_Tag_tbl", "Expense_tbl", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}