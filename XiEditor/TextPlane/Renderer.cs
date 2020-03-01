using System;
using System.Drawing.Text;
using AVFoundation;
using OpenTK.Graphics.OpenGL;

namespace XiEditor
{
    public class Renderer
    {
        //var vertexArrayIds = [GLuint](repeating: 0, count: 2)
        //var bufferIds = [GLuint](repeating: 0, count: 4)
        //let vertexPositionBufId = 0
        //let elementIndexBufId = 1
        //let solidInstanceBufId = 2
        //let textInstanceBufId = 3

        readonly ShaderProgram _solidProgram;
        //var solid_u_scale: GLuint = 0
        //let maxSolidInstances = 4096
        //let solidInstanceSize = 8 // in floats
        //var solidInstances: [GLfloat]
        //    var solidInstanceIx = 0

        readonly Atlas _atlas;
        readonly ShaderProgram _textProgram;
        //var text_u_scale: GLuint = 0
        //let maxTextInstances = 65536
        //let textInstanceSize = 12 // in floats
        //var textInstances: [GLfloat]
        //    var textInstanceIx = 0

        //var drawState: DrawState = .none
        //var u_x_scale: GLfloat = 0
        //var u_y_scale: GLfloat = 0
        //var dpiScale: CGFloat = 0


        public Renderer()
        {
            // solid rectangle rendering
            _solidProgram = new ShaderProgram();
            _solidProgram.AttachShader("solid.v", ShaderType.VertexShader);
            _solidProgram.AttachShader("solid.f", ShaderType.FragmentShader);
            _solidProgram.Link();

            //todo finish solid rectange rendering...

            // vertext position buffer

            // element buffer

            // instance buffer

            // rect origin

            // rect size

            // rgba

            // text blending

            _textProgram = new ShaderProgram();
            _textProgram.AttachShader("text.v", ShaderType.VertexShader);
            _textProgram.AttachShader("text.f", ShaderType.FragmentShader);
            _textProgram.Link();

            // todo finish text blending ...

            // rect origin

            // rect size

            // rgba

            // uvorigin

            // uv size

            _atlas = new Atlas();
        }

    }
}
