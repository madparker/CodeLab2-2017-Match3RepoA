using UnityEngine;
using EasingEquations;

[CreateAssetMenu (menuName = "Easing Properties")]
public class EasingProperties : ScriptableObject
{
    [SerializeField] private Easing.FunctionType _Easing;
    public Easing.Function MovementEasing { get { return Easing.GetFunctionWithTypeEnum(_Easing); }}
}
