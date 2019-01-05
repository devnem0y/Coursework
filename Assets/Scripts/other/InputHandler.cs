using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler
{
    private ScreenManager sm;

    public InputHandler(ScreenManager sm)
    {
        this.sm = sm;

        AddListener();
    }

    private void AddListener() // Получить кнопку и добавить ей обработчик
    {
        if (sm.GUIM != null)
        {
            sm.GUIM.play.onClick.AddListener(() => ScreenTrackMap());
            sm.GUIM.trackMapGarage.onClick.AddListener(() => ScreenTrackMap());
            sm.GUIM.quit.onClick.AddListener(() => Quit());
            sm.GUIM.backAppExit.onClick.AddListener(() => QuitBack());
            sm.GUIM.okAppExit.onClick.AddListener(() => QuitOk());
            sm.GUIM.menu.onClick.AddListener(() => Menu());
            sm.GUIM.garage.onClick.AddListener(() => Garage());
            sm.GUIM.race.onClick.AddListener(() => Race());
            sm.GUIM.okRaceStart.onClick.AddListener(() => RaceStart());
            sm.GUIM.backRaceStart.onClick.AddListener(() => RaceStartBack());
            sm.GUIM.settings.onClick.AddListener(() => Settings());
            sm.GUIM.backSettings.onClick.AddListener(() => BackSettings());
        }

        if (sm.GUIL != null)
        {
            sm.GUIL.trackMapGameOver.onClick.AddListener(() => ScreenTrackMap());
            sm.GUIL.okWin.onClick.AddListener(() => ScreenTrackMap());
            sm.GUIL.settings.onClick.AddListener(() => Settings());
            sm.GUIL.backSettings.onClick.AddListener(() => BackSettings());
            sm.GUIL.resume.onClick.AddListener(() => Resume());
            sm.GUIL.replayGameOver.onClick.AddListener(() => RaceStart());
            sm.GUIL.replayPause.onClick.AddListener(() => RaceStart());
            sm.GUIL.pause.onClick.AddListener(() => Pause());
        }
    }

    private void ScreenTrackMap()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Main"))
        {
            // Если сцена не является главной, то делаем сохранение, запускаем сцену Main и устанавливаем состояние TrackMap
            // save all parametrs
            SceneManager.LoadScene("Main");
        }

        sm.SetState(new TrackMap());
    }

    private void Settings()
    {
        sm.SetState(new Settings());
    }

    private void BackSettings()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Main"))
        {
            sm.GUIL.settingsP.SetActive(false);
            Pause();
        }
        else
        {
            sm.GUIM.settingsP.SetActive(false);
            sm.SetState(new Menu());
        } 
    }

    private void Pause()
    {
        Time.timeScale = 0;
        sm.ShowDialog(sm.GUIL.pauseP);
    }

    private void Quit()
    {
        sm.ShowDialog(sm.GUIM.appExitP);
    }

    private void QuitOk()
    {
        Application.Quit();
    }

    private void QuitBack()
    {
        sm.GUIM.appExitP.SetActive(false);
    }

    private void Menu()
    {
        sm.SetState(new Menu());
    }

    private void Garage()
    {
        sm.SetState(new Garage());
    }

    private void Race()
    {
        sm.ShowDialog(sm.GUIM.raceStartP);
    }

    private void RaceStart()
    {
        SceneManager.LoadScene("Level1"); // 
    }

    private void RaceStartBack()
    {
        sm.GUIM.raceStartP.SetActive(false);
    }

    private void Resume()
    {
        sm.SetState(new Game());
    }
}
