using store.core.src.Interface;

namespace store.api.src.Decorator;

public abstract class ProductDecorator : IProduct
{
    protected readonly IProduct _wrapped;

    protected ProductDecorator(IProduct product)
    {
        _wrapped = product;
    }

    public abstract decimal GetPrezzo();
    public abstract string Descrizione();
    public abstract string NomeDecorator { get; }

}

public class GiftWrapDecorator : ProductDecorator
{
    public GiftWrapDecorator(IProduct p) : base(p) { }

    public override string NomeDecorator => "Confezione regalo";
    public override decimal GetPrezzo()  => _wrapped.GetPrezzo() + 2.00m;
    public override string Descrizione() => _wrapped.Descrizione() + " + Confezione regalo (+2€)";
}

public class ExpressDeliveryDecorator : ProductDecorator
{
    public ExpressDeliveryDecorator(IProduct p) : base(p) { }

    public override string NomeDecorator => "Consegna express";
    public override decimal GetPrezzo()  => _wrapped.GetPrezzo() + 5.00m;
    public override string Descrizione() => _wrapped.Descrizione() + " + Consegna express (+5€)";
}

public class InsuranceDecorator : ProductDecorator
{
    public InsuranceDecorator(IProduct p) : base(p) { }

    public override string NomeDecorator => "Assicurazione";
    public override decimal GetPrezzo()  => _wrapped.GetPrezzo() + 3.00m;
    public override string Descrizione() => _wrapped.Descrizione() + " + Assicurazione (+3€)";
}