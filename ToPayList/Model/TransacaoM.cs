using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToPayList.Model
{
    [dNet.DB.Database(Name = "Transacao")]
    public class TransacaoM : dNet.DB.dModel
    {
        [dNet.DB.Column(DbName = "IdTransacao", IsPrimaryKey = true, IsAutoIncrement=true)]
        public decimal IdTransacao
        {
            get { return _IdTransacao; }
            set
            {
                if (_IdTransacao != value)
                {
                    _IdTransacao = value;
                    IdTransacaoModified = true;
                    this.OnPropertyChanged("IdTransacao");
                }
            }
        }
        private decimal _IdTransacao;
        public bool IdTransacaoModified { get; set; }

        [dNet.DB.Column(DbName = "Descricao", IsDescription=true)]
        public string Descricao
        {
            get { return _Descricao; }
            set
            {
                if (_Descricao != value)
                {
                    _Descricao = value;
                    DescricaoModified = true;
                    this.OnPropertyChanged("Descricao");
                }
            }
        }
        private string _Descricao;
        public bool DescricaoModified { get; set; }

        [dNet.DB.Column(DbName = "Data")]
        public DateTime Data
        {
            get { return _Data; }
            set
            {
                if (_Data != value)
                {
                    _Data = value;
                    DataModified = true;
                    this.OnPropertyChanged("Data");
                }
            }
        }
        private DateTime _Data;
        public bool DataModified { get; set; }

        [dNet.DB.Column(DbName = "Valor")]
        public decimal Valor
        {
            get { return _Valor; }
            set
            {
                if (_Valor != value)
                {
                    _Valor = value;
                    ValorModified = true;
                    this.OnPropertyChanged("Valor");
                }
            }
        }
        private decimal _Valor;
        public bool ValorModified { get; set; }

        [dNet.DB.Column(DbName = "Pago")]
        public int Pago
        {
            get { return _Pago; }
            set
            {
                if (_Pago != value)
                {
                    _Pago = value;
                    PagoModified = true;
                    this.OnPropertyChanged("Pago");
                }
            }
        }
        private int _Pago;
        public bool PagoModified { get; set; }

        [dNet.DB.Column(DbName = "Comprovante")]
        public decimal Comprovante
        {
            get { return _Comprovante; }
            set
            {
                if (_Comprovante != value)
                {
                    _Comprovante = value;
                    ComprovanteModified = true;
                    this.OnPropertyChanged("Comprovante");
                }
            }
        }
        private decimal _Comprovante;
        public bool ComprovanteModified { get; set; }
    }
}
