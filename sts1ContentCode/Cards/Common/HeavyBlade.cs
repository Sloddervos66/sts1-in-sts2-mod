using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace sts1Content.sts1ContentCode.Cards.Common;

[Pool(typeof(IroncladCardPool))]
public sealed class HeavyBlade() : CustomCard(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<StrengthPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new("Multiplier", 3m),
        new CalculationBaseVar(14m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) =>
        {
            var strength = card.Owner.Creature.GetPowerAmount<StrengthPower>();
            return strength * (card.DynamicVars["Multiplier"].BaseValue - 1);
        })
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var target = cardPlay.Target;
        if (target == null) return;
        
        await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this).Targeting(target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Multiplier"].UpgradeValueBy(2m);
    }
}