using Player47.Core.Service;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Player47.UI
{
    public class AutoScrollCredits : MonoBehaviour
    {
        [Header("ScrollRect reference")]
        public ScrollRect scroll;

        [Header("Scroll duration in seconds")]
        public float totalTime = 20f;

        [Header("Optional easing curve (0->1)")]
        public AnimationCurve progressCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [Header("Debug")]
        public bool enableDebugLogs = true;

        [Header("Next Scene")]
        public string nextSceneName = "Menu";

        // internal
        bool isScrolling = false;
        float timer = 0f;

        IEnumerator Start()
        {
            if (scroll == null)
            {
                Debug.LogError("[AutoScrollCreditsRobustV2] ScrollRect não atribuído.");
                yield break;
            }

            // Desativa interação do ScrollRect pra evitar conflito com entrada do usuário
            scroll.horizontal = false;
            scroll.vertical = true;
            scroll.movementType = ScrollRect.MovementType.Clamped;
            scroll.inertia = false;

            // Espera um frame para o Unity construir a UI básica
            yield return null;

            // Força atualização de canvas/layout (resolve problemas com ContentSizeFitter / LayoutGroup)
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(scroll.content);
            yield return new WaitForEndOfFrame(); // garante que tudo foi aplicado

            // Debug: imprime tamanhos
            float contentH = scroll.content.rect.height;
            float viewportH = scroll.viewport.rect.height;
            if (enableDebugLogs)
            {
                Debug.Log($"[AutoScrollCreditsRobustV2] Content height: {contentH:F1}, Viewport height: {viewportH:F1}");
            }

            // Se o conteúdo for menor ou igual à viewport, não há rolagem possível
            if (contentH <= viewportH + 0.01f)
            {
                if (enableDebugLogs)
                    Debug.LogWarning("[AutoScrollCreditsRobustV2] Conteúdo menor/igual que viewport — rolagem não necessária.");
                yield break;
            }

            // Começa no topo (1) — garantimos com um pequeno atraso caso algo ainda tente sobrescrever
            scroll.verticalNormalizedPosition = 1f;
            yield return null;
            scroll.verticalNormalizedPosition = 1f;

            // Inicia a corrotina de rolagem
            isScrolling = true;
            timer = 0f;
        }

        void Update()
        {
            if (!isScrolling || scroll == null) return;

            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / Mathf.Max(0.0001f, totalTime));
            float curveVal = progressCurve.Evaluate(t);

            // aplica rolagem: 1 -> 0
            scroll.verticalNormalizedPosition = Mathf.Lerp(1f, 0f, curveVal);

            if (enableDebugLogs && (t == 0f || t >= 1f || (int)(timer * 10) % 10 == 0))
            {
                // Log esparso pra não encher o console; mostra começo, fim e intervalos
                Debug.Log($"[AutoScrollCreditsRobustV2] progress {t:F3} verticalNormalizedPosition {scroll.verticalNormalizedPosition:F3}");
            }

            if (t >= 1f)
            {
                isScrolling = false;
                OnFinish();
            }
        }

        void OnFinish()
        {
            if (enableDebugLogs) Debug.Log("[AutoScrollCreditsRobustV2] Rolagem finalizada.");
            // ações finais (desativar painel, carregar cena, etc.) podem ser colocadas aqui
            SceneLoader.Instance.LoadSceneAsync(nextSceneName);
        }
    }
}