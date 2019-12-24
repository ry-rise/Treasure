﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ParameterBase parameter = null;
    /// <summary>
    /// ゲームのパラメーター
    /// </summary>
    public ParameterBase Parameter { get { return parameter; } }
    private void Awake()
    {
        Application.targetFrameRate = 72;
    }
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        parameter.Life = parameter.MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (!parameter.StartGameFlag) return;
        if (parameter.EndGameFlag) return;
        GameOver();
        CountTimer();
        BeamEnergyRecharge();
    }
    /// <summary>
    /// ゲームスタート
    /// </summary>
    public void GameStart()
    {
        parameter.StartGameFlag = true;
    }
    public void Damage(float amount)
    {
        parameter.Life -= amount;
    }
    private void GameOver()
    {
        if (parameter.Life <= 0)
        {
            parameter.EndGameFlag = true;
            parameter.Life = 0;
        }
    }
    /// <summary>
    /// ビームエネルギーを使う
    /// </summary>
    /// <returns></returns>
    public void UseEnergy()
    {
        if (parameter.BeamEnergy > 0)
        {
            parameter.BeamEnergy -= Time.deltaTime / parameter.MaxBeamLimitTime;
        }
        else
        {
            parameter.BeamEnergy = 0;
            parameter.IsRecharged = true;
        }
    }
    /// <summary>
    /// ビームの再装填
    /// </summary>
    private void BeamEnergyRecharge()
    {
        if (parameter.IsRecharged)
        {
            parameter.BeamEnergy += Time.deltaTime / parameter.MaxBeamEnergyRechargeTime;
            if (parameter.BeamEnergy >= 1f)
            {
                parameter.IsRecharged = false;
                parameter.BeamEnergy = 1f;
            }
        }
    }
    private void CountTimer()
    {
        parameter.CurrentPlayTime += Time.deltaTime;
        if (parameter.MaxPlayTime - parameter.CurrentPlayTime <= 0)
        {
            parameter.EndGameFlag = true;
            parameter.CurrentPlayTime = parameter.MaxPlayTime;
        }
    }
    [System.Serializable]
    public class TurnTimes
    {
        [SerializeField, Tooltip("フェイドアウト開始の待ち時間")]
        private float fadeOutStop = 0.1f;
        public float FadeOutStop { get { return fadeOutStop; } }
        [SerializeField, Tooltip("フェイドアウト開始からの必要時間")]
        private float fadeOut = 0.25f;
        public float FadeOut { get { return fadeOut; } }
        [SerializeField, Tooltip("フェイドイン開始の待ち時間")]
        private float fadeInStop = 0.5f;
        public float FadeInStop { get { return fadeInStop; } }
        [SerializeField, Tooltip("フェイドイン開始からの必要時間")]
        private float fadeIn = 0.25f;
        public float FadeIn { get { return fadeIn; } }
        [SerializeField, Tooltip("フェイドイン終了後の経過時間")]
        private float end = 0.1f;
        public float End { get { return end; } }
    }
    [System.Serializable]
    public class ParameterBase
    {
        [SerializeField, Tooltip("移動速度")]
        private float moveSpeed = 5;
        /// <summary>
        /// 移動速度
        /// </summary>
        public float MoveSpeed { get { return moveSpeed; } }
        [SerializeField, Tooltip("旋回にかかる時間")]
        private TurnTimes turnTime = null;
        /// <summary>
        /// 旋回にかかる時間
        /// </summary>
        public TurnTimes TurnTime { get { return turnTime; } }
        [SerializeField, Tooltip("プレイ時間")]
        private float maxPlayTime = 180f;
        /// <summary>
        /// プレイ時間
        /// </summary>
        public float MaxPlayTime { get { return maxPlayTime; } }
        [SerializeField, Tooltip("最大体力")]
        private float maxLife = 100;
        /// <summary>
        /// 最大体力
        /// </summary>
        public float MaxLife { get { return maxLife; } }
        [SerializeField, Tooltip("ライトの明るさ")]
        private float luxVolume = 1;
        /// <summary>
        /// ライトの明るさ
        /// </summary>
        public float LuxVolume { get { return luxVolume; } }
        [SerializeField, Tooltip("ビーム継続時間")]
        private float maxBeamLimitTime = 3f;
        /// <summary>
        /// ビーム継続時間
        /// </summary>
        public float MaxBeamLimitTime { get { return maxBeamLimitTime; } }
        [SerializeField, Tooltip("ビーム再装填時間")]
        private float maxBeamEnergyRechargeTime = 5f;
        /// <summary>
        /// ビーム再装填時間
        /// </summary>
        public float MaxBeamEnergyRechargeTime { get { return maxBeamEnergyRechargeTime; } }
        [SerializeField, Tooltip("鈍足継続時間")]
        private float maxSlowTime = 5f;
        /// <summary>
        /// 鈍足継続時間
        /// </summary>
        public float MaxSlowTime { get { return maxSlowTime; } }
        [SerializeField, Range(0f, 1f), Tooltip("鈍足時の減速率")]
        private float slowMagnification = 0.75f;
        /// <summary>
        /// 鈍足時の減速率
        /// </summary>
        public float SlowMagnification { get { return slowMagnification; } }
        [SerializeField, Tooltip("視界妨害時間")]
        private float maxObstructViewTime = 5f;
        /// <summary>
        /// 視界妨害時間
        /// </summary>
        public float MaxObstructViewTime { get { return maxObstructViewTime; } }
        [SerializeField, Range(0f, 1f), Tooltip("視界妨害域（画面の割合）")]
        private float extentObstructView = 0.6f;
        /// <summary>
        /// 視界妨害域（画面の割合）
        /// </summary>
        public float ExtentObstructView { get { return extentObstructView; } }
        [SerializeField, Range(0f, 1f), Tooltip("視界妨害の泥のアルファ値")]
        private float alphaObstructView = 0.5f;
        /// <summary>
        /// 視界妨害の泥のアルファ値
        /// </summary>
        public float AlphaObstructView { get { return alphaObstructView; } }
        //[SerializeField, Tooltip("泥のアルファ値の変化量")]
        //private float alphaSpeedObstructView = 1f;
        /// <summary>
        /// 現在体力
        /// </summary>
        public float Life { get; set; }
        /// <summary>
        /// 経過時間
        /// </summary>
        public float CurrentPlayTime { get; set; }
        [SerializeField]
        private Vector3 playerPosition = Vector3.zero;
        public Vector3 PlayerPosition { get { return playerPosition; } }
        [SerializeField]
        private Vector3 handPosition = Vector3.zero;
        public Vector3 HandPosition
        {
            get { return handPosition; }
        }
            [SerializeField]
        private Player player = null;
        /// <summary>
        /// プレイヤーの向き
        /// </summary>
        public float Direction { get { return player.transform.rotation.y; } }
        /// <summary>
        /// エネルギー量
        /// </summary>
        public float BeamEnergy { get; set; } = 1f;
        public bool IsRecharged { get; set; } = false;
        /// <summary>
        /// ゲームが始まったかどうか
        /// </summary>
        public bool StartGameFlag { get; set; } = false;
        /// <summary>
        /// ビーム撃てるかのフラグ
        /// </summary>
        public bool BeamFlag{ get { return !IsRecharged; } }
        /// <summary>
        /// ゲームが終わったかどうか
        /// </summary>
        public bool EndGameFlag { get; set; } = false;
    }
}
