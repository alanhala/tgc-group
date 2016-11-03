﻿using System.Drawing;
using TGC.Core.Example;
using TGC.Core.SceneLoader;
using TGC.Group.Model.Camera;
using TGC.Group.Model.Particles;

namespace TGC.Group.Model
{
    class Scenario : TgcExample
    {
        private TgcScene scene;
        private Car car;
        private TwistedCamera camera;
        private Velocimetro velocimetro;
        private SmokeParticle emitter;

        public Scenario(string mediaDir, string shadersDir) : base(mediaDir, shadersDir)
        {
            Category = Game.Default.Category;
            Name = Game.Default.Name;
            Description = Game.Default.Description;
        }

        public override void Init()
        {
            var loader = new TgcSceneLoader();
            scene = loader.loadSceneFromFile(MediaDir + "city-TgcScene.xml");
            car = new Car(scene);
            camera = new TwistedCamera(Input, car, 100f, 250f);
            Camara = camera;
            velocimetro = new Velocimetro();
            emitter = new SmokeParticle(car);
        }

        public override void Update()
        {
            PreUpdate();
            car.move(Input, ElapsedTime);
            emitter.update();
        }

        public override void Render()
        {
            PreRender();
            car.render();
            scene.renderAll();
            velocimetro.render(DrawText, car.getVelocity());
            DrawText.drawText("Energy: " + car.getEnergy(), 800, 600, Color.Yellow);
            emitter.render(ElapsedTime);
            PostRender();
        }

        public override void Dispose()
        {
            scene.disposeAll();
            car.dispose();
        }
    }
}
