using System.Diagnostics.Contracts;

public interface IOngoingFormModifier
{	
	public IOngoingFormModifier Create(BattleFormReferences references);

	public bool Destroy(BattleFormReferences references);
}