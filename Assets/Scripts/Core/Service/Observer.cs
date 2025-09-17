using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player47.Core.Service
{
    /// <summary>
    /// Implementa o padrão Observer para monitorar mudanças em um valor do tipo <typeparamref name="T"/>.
    /// 
    /// - Permite observar valores genéricos (int, float, string, objetos).
    /// - Dispara eventos sempre que o valor observado é alterado.
    /// - Suporta múltiplos ouvintes/listeners que podem se inscrever ou se desinscrever dinamicamente.
    /// 
    /// Exemplo de uso:
    /// <code>
    /// Observer<int> health = new Observer<int>(100);
    /// health.AddListener(newHealth => Debug.Log($"Vida: {newHealth}"));
    /// health.Value -= 20; // Dispara o evento e notifica os inscritos
    /// </code>
    /// </summary>
    [Serializable]
    public class Observer<T>
    {
        [SerializeField] T _value;
        [SerializeField] UnityEvent<T> _onValueChanged;

        /// <summary>
        /// Valor atual observado.
        /// Ao ser alterado por <see cref="Set"/> ou pela propriedade <see cref="Value"/>, 
        /// dispara o evento de notificação caso seja diferente do valor anterior.
        /// </summary>
        public T Value
        {
            get => _value;
            set => Set(value);
        }

        /// <summary>
        /// Construtor do Observer.
        /// </summary>
        /// <param name="value">Valor inicial a ser observado.</param>
        /// <param name="callback">Callback opcional a ser inscrito imediatamente.</param>
        public Observer(T value, UnityAction<T> callback = null)
        {
            this._value = value;

            _onValueChanged = new UnityEvent<T>();

            if (callback != null)
                _onValueChanged.AddListener(callback);
        }

        /// <summary>
        /// Define um novo valor para o Observer.
        /// Caso o valor seja diferente do anterior, notifica todos os ouvintes inscritos.
        /// </summary>
        /// <param name="value">Novo valor a ser definido.</param>
        public void Set(T value)
        {
            if (Equals(_value, value)) return;

            this._value = value;
            Invoke();
        }

        /// <summary>
        /// Invoca manualmente o evento de mudança, notificando todos os ouvintes 
        /// com o valor atual.
        /// </summary>
        public void Invoke()
        {
            Debug.Log($"Invoking {_onValueChanged.GetPersistentEventCount()} listeners");
            _onValueChanged?.Invoke(_value);
        }

        /// <summary>
        /// Adiciona um novo ouvinte (listener) ao evento de mudança.
        /// </summary>
        /// <param name="callback">Método a ser chamado quando o valor mudar.</param>
        public void AddListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) _onValueChanged = new UnityEvent<T>();

            _onValueChanged.AddListener(callback);
        }

        /// <summary>
        /// Remove um ouvinte específico do evento de mudança.
        /// </summary>
        /// <param name="callback">Callback previamente inscrito.</param>
        public void RemoveListener(UnityAction<T> callback)
        {
            if (callback == null) return;
            if (_onValueChanged == null) return;

            _onValueChanged.RemoveListener(callback);
        }

        /// <summary>
        /// Remove todos os ouvintes atualmente inscritos no evento.
        /// </summary>
        public void RemoveAllListeners()
        {
            if (_onValueChanged == null) return;

            _onValueChanged.RemoveAllListeners();
        }

        /// <summary>
        /// Libera os recursos do Observer:
        /// - Remove todos os ouvintes;
        /// - Reseta o valor para <c>default(T)</c>;
        /// - Remove a referência ao evento.
        /// </summary>
        public void Dispose()
        {
            RemoveAllListeners();
            _onValueChanged = null;
            Value = default;
        }
    }
}
