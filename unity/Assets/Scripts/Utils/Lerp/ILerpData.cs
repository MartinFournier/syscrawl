namespace syscrawl.Utils.Lerp
{
    public interface ILerpData<T>
    {
        T From { get; set; }

        T To { get; set; }

        T Current { get; set; }

        T Evaluate(float t);
    }
}

