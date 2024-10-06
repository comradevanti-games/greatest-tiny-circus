using UnityEngine;

namespace GTC.Level
{
    public record Level(
        GameObject Root,
        GameObject Flea,
        GameObject JumpTarget);
}