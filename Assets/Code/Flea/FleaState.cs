#nullable enable

namespace GTC.Flea
{
    public abstract record FleaState
    {
        public record PreparingForJump : FleaState;

        public record Flying : FleaState;

        public record Failed : FleaState;

        public record Success : FleaState;
    }
}