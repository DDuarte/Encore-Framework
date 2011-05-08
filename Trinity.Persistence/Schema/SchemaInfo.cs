using System;
using System.Diagnostics.Contracts;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace Trinity.Persistence.Schema
{
    public sealed class SchemaInfo
    {
        private readonly SchemaExport _schema;

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(_schema != null);
        }

        public SchemaInfo(DatabaseContext ctx)
        {
            Contract.Requires(ctx != null);

            _schema = new SchemaExport(ctx.Configuration);
        }

        public void Create()
        {
            _schema.Create(false, true);
        }

        public void Drop()
        {
            _schema.Drop(false, true);
        }

        public void Export(string file)
        {
            Contract.Requires(!string.IsNullOrEmpty(file));

            _schema.SetOutputFile(file);
            _schema.Create(false, false);
        }
    }
}
