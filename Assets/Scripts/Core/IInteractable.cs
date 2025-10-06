namespace Player47.Core
{
	/// <summary>
	/// Significa que o objeto pode ser interagido e possui o método <see cref="Interact"/>
	/// </summary>
	public interface IInteractable
	{
		string TranslateKey { get; }
		bool CanShowText { get; }

        void Interact();
	} 
}