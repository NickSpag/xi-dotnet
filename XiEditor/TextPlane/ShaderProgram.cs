using System;
using System.IO;
using Foundation;
using OpenTK.Graphics.OpenGL;

namespace XiEditor
{
    public class ShaderProgram
    {
        readonly int program;

        //todo i believe there's an idispose equivalent
        //find how many times the renderer is used and how, run GL.DeleteProgram(program) on closure. 

        public ShaderProgram()
        {
            program = GL.CreateProgram();
            if(program == 0)
            {
                throw new Exception();
            }
        }

        public void AttachShader(string name, ShaderType type)
        {
            var path = GetShaderPaths(name);

            var data = NSData.FromFile(path);
            var shader = GL.CreateShader(type);

            var bytes = data.Bytes.ToString();

            GL.ShaderSource(shader, bytes);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int result);

            if(result == 0)
            {
                GL.AttachShader(program, shader);
            }
            else
            {
                //todo log
                //var length: GLint = 0
                //glGetShaderiv(shader, GLenum(GL_INFO_LOG_LENGTH), &length)
                //var str = [GLchar](repeating: GLchar(0), count: Int(length) + 1)
                //var size: GLsizei = 0
                //glGetShaderInfoLog(shader, GLsizei(length), &size, &str)
                //let msg = String(cString: str)
                //NSLog("compilation of \(name) failed: \(msg)")
            }
        }

        public void Link()
        {
            GL.LinkProgram(program);
            GL.GetProgram(program, ProgramParameter.LinkStatus, out int param);

            if(param == 0)
            {
                System.Console.WriteLine("Shader linking error");
                throw new Exception();
            }
        }

        public void Use() => GL.UseProgram(program);

        public int? GetUniformLocation(string name) => GL.GetUniformLocation(program, name);

        private string GetShaderPaths(string name)
        {
            return "";
//            var path = "";

            //todo


        }
    }
}
